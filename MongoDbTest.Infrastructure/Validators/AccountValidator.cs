using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Validators
{
    public class AccountValidator: IAccountValidator
    {
        private readonly List<IValidator> _validators = new List<IValidator>();
        private readonly List<ValidationResult> _results = new List<ValidationResult>();
        private readonly IAccountApiClient _accountApiClient;
        private readonly List<ValidatorResult> _allResults = new List<ValidatorResult>();
        public IEnumerable<ValidatorResult> Results => _allResults;

        public AccountValidator(IAccountApiClient accountApiClient)
        {
            _accountApiClient = accountApiClient;

            var _accountExistValidator = new AccountExistValidator(_accountApiClient);
            var _accountLimitValidator = new AccountLimitValidator();
            var _accountProviderValidator = new AccountProviderValidator();
            var _accountProductValidator = new AccountProductValidator();

            _validators.Add(_accountExistValidator);
            _validators.Add(_accountLimitValidator);
            _validators.Add(_accountProviderValidator);
            _validators.Add(_accountProductValidator);
        }

        public async Task<bool> ValidateAsync(Account account)
        {
            var context = new ValidationContext<Account>(account);
            IEnumerable<Task<ValidationResult>> tasks = _validators.Select(val => val.ValidateAsync(context));

            var validationResults = await Task.WhenAll(tasks);
            _results.AddRange(validationResults.ToList());

            foreach (var vr in validationResults)
            {
                _allResults.Add(new ValidatorResult(vr));
            }

            return _allResults.All(r => r.IsValid);
        }
    }
}