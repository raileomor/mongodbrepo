using System.Net.Http;
using FluentValidation;
using FluentValidation.Results;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;
using MongoDbTest.Infrastructure.RestClients;
using Newtonsoft.Json;

namespace MongoDbTest.Infrastructure.Validators
{
    public class AccountExistValidator : AbstractValidator<Account>, IAccountExistValidator
    {
        private readonly IAccountApiClient _documentApiClient;
        public AccountExistValidator(IAccountApiClient documentApiClient) {
            _documentApiClient = documentApiClient;
            RuleFor(x => x.Id).CustomAsync(async (id, context, cancellation) => {
                Account account = await _documentApiClient.GetAccountByIdAsync(id);
                if (account != null)
                    context.AddFailure("GetAccount", JsonConvert.SerializeObject(account));
            });
        }
    }
}