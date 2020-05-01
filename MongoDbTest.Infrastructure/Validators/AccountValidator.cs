using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Validators
{
    public class AccountValidator: AbstractValidator<Account>, IAccountValidator
    {
        private readonly IAccountExistValidator _accountExistValidator;
        private readonly IAccountLimitValidator _accountLimitValidator;
        private readonly IAccountProviderValidator _accountProviderValidator;
        private readonly IAccountProductValidator _accountProductValidator;
        private readonly IAccountValidationRule _accountValidationRule;

        public AccountValidator(IAccountValidationRule accountValidationRule,
                                IAccountExistValidator accountExistValidator,
                                IAccountProviderValidator accountProviderValidator,
                                IAccountLimitValidator accountLimitValidator,
                                IAccountProductValidator accountProductValidator)
        {
            _accountValidationRule = accountValidationRule;
            _accountExistValidator = accountExistValidator;
            _accountLimitValidator = accountLimitValidator;
            _accountProviderValidator = accountProviderValidator;
            _accountProductValidator = accountProductValidator;

            _accountValidationRule.Add(_accountExistValidator);
            _accountValidationRule.Add(_accountLimitValidator);
            _accountValidationRule.Add(_accountProviderValidator);
            _accountValidationRule.Add(_accountProductValidator);

            AddRule(_accountValidationRule);
        }

        public IEnumerable<ValidationResult> Results => _accountValidationRule.Results;
    }
}