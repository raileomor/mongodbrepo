using System.Collections.Generic;
using System.Net.Http;
using FluentValidation;
using FluentValidation.Results;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;
using MongoDbTest.Infrastructure.RestClients;
using Newtonsoft.Json;

namespace MongoDbTest.Infrastructure.Validators
{
    public class AccountExistValidator : AbstractValidator<Account>, IValidatorResult
    {
        public string Rule => "AccountExist";
        private readonly IAccountApiClient _documentApiClient;
        public object ServiceResult { get; private set;}

        public AccountExistValidator(IAccountApiClient documentApiClient) {
            _documentApiClient = documentApiClient;
            RuleFor(x => x.Id).CustomAsync(async (id, context, cancellation) => {
                ServiceResult = await _documentApiClient.GetAccountByIdAsync(id);
                if (ServiceResult == null)
                    context.AddFailure("Account not found");
            });
        }
    }
}