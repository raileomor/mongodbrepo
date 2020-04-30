using System.Net.Http;
using FluentValidation;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;
using MongoDbTest.Infrastructure.RestClients;

namespace MongoDbTest.Infrastructure.Validators
{
    public class AccountExistValidator : AbstractValidator<Account>, IAccountExistValidator
    {
        private readonly IAccountApiClient _documentApiClient;
        public AccountExistValidator(IAccountApiClient documentApiClient) {
            _documentApiClient = documentApiClient;
            RuleFor(x => x.Id).MustAsync(async (id, cancellation) => {
                return await _documentApiClient.ExistAsync(id);
            }).WithMessage("Account Id don't exist");
        }
    }
}