using FluentValidation;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Validators
{
    public class DocumentValidator: AbstractValidator<Account>
    {
        public DocumentValidator()
        {
            //AddRule(new CompositeValidatorRule(new DocumentLimitValidator(), new DocumentProviderValidator(), new DocumentExistValidator()));
            Include(new DocumentLimitValidator());
            Include(new DocumentProviderValidator());
            Include(new DocumentExistValidator());
        }
    }
}