using System.Net.Http;
using FluentValidation;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;
using MongoDbTest.Infrastructure.RestClients;

namespace MongoDbTest.Infrastructure.Validators
{
    public class DocumentExistValidator : AbstractValidator<Account>
    {
        public DocumentExistValidator() {
            RuleFor(x => x.Id).MustAsync(async (id, cancellation) => {
                IDocumentApiClient _client = new DocumentApiClient(new HttpClient());
                return await _client.ExistAsync(id);
            }).WithMessage("ID Must be unique");
        }
    }
}