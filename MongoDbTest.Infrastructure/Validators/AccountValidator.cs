using FluentValidation;
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

        public AccountValidator(IAccountExistValidator accountExistValidator,
                                IAccountProviderValidator accountProviderValidator,
                                IAccountLimitValidator accountLimitValidator,
                                IAccountProductValidator accountProductValidator)
        {
            _accountExistValidator = accountExistValidator;
            _accountLimitValidator = accountLimitValidator;
            _accountProviderValidator = accountProviderValidator;
            _accountProductValidator = accountProductValidator;
            AddRule(new CompositeValidatorRule(_accountExistValidator,
                                            _accountLimitValidator,
                                            _accountProviderValidator,
                                            _accountProductValidator));
        }
    }
}