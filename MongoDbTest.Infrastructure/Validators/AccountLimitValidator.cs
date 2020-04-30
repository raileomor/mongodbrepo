using FluentValidation;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Validators
{
    public class AccountLimitValidator: AbstractValidator<Account>, IAccountLimitValidator
    {
        public AccountLimitValidator()
        {
            RuleFor(x => x.Limit).GreaterThan(10000);
        }
    }
}