using FluentValidation;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Validators
{
    public class AccountLimitValidator: AbstractValidator<Account>, IValidatorResult
    {
        public string Rule => "LimitGreatherThan1000";
        public object ServiceResult => null;

        public AccountLimitValidator()
        {
            RuleFor(x => x.Limit).GreaterThan(10000);
        }
    }
}