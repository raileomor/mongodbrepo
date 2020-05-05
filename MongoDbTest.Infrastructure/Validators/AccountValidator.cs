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
        private readonly IAccountApiClient _accountApiClient;
        private readonly List<ValidatorResult> _results = new List<ValidatorResult>();
        public IEnumerable<ValidatorResult> Results => _results;

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
            List<Task<ValidationResult>> tasks = new List<Task<ValidationResult>>();

            for (int i=0;i<_validators.Count;i++)
            {
                tasks.Add(_validators[i].ValidateAsync(context));
            }

            await Task.WhenAll(tasks);

            for (int i=0;i<tasks.Count;i++)
            {
                _results.Add(new ValidatorResult(tasks[i].Result, _validators[i]));
            }

            return _results.All(r => r.IsValid);
        }
    }
}