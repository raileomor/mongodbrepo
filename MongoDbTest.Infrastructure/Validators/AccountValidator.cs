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

        public AccountValidator(IAccountExistValidator accountExistValidator,
                                IAccountProviderValidator accountProviderValidator,
                                IAccountLimitValidator accountLimitValidator)
        {
            _accountExistValidator = accountExistValidator;
            _accountLimitValidator = accountLimitValidator;
            _accountProviderValidator = accountProviderValidator;
            //AddRule(new CompositeValidatorRule(new DocumentLimitValidator(), new DocumentProviderValidator(), new DocumentExistValidator()));
            Include(_accountProviderValidator);
            Include(_accountLimitValidator);
            Include(_accountExistValidator);
        }
    }
}