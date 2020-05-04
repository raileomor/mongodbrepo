using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Validators
{
    public class AccountValidator: AbstractValidator<Account>, IAccountValidator
    {
        private readonly IAccountApiClient _accountApiClient;
        private readonly IAccountValidationRule _accountValidationRule;
        public IEnumerable<ValidationResult> Results => _accountValidationRule.Results;

        public AccountValidator(IAccountApiClient accountApiClient)
        {
            _accountApiClient = accountApiClient;
            _accountValidationRule = new AccountValidatorRules();

            var _accountExistValidator = new AccountExistValidator(_accountApiClient);
            var _accountLimitValidator = new AccountLimitValidator();
            var _accountProviderValidator = new AccountProviderValidator();
            var _accountProductValidator = new AccountProductValidator();

            _accountValidationRule.Add(_accountExistValidator);
            _accountValidationRule.Add(_accountLimitValidator);
            _accountValidationRule.Add(_accountProviderValidator);
            _accountValidationRule.Add(_accountProductValidator);

            AddRule(_accountValidationRule);
        }
    }
}