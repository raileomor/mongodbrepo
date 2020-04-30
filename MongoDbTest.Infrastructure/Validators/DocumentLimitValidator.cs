using FluentValidation;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Validators
{
    public class DocumentLimitValidator: AbstractValidator<Account>
    {
        public DocumentLimitValidator()
        {
            RuleFor(x => x.Limit).GreaterThan(10000);
        }
    }
}