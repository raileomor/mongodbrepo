using System;
using FluentValidation;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Validators
{
    public class AccountProviderValidator: AbstractValidator<Account>, IAccountProviderValidator
    {
        public AccountProviderValidator()
        {
            RuleFor(x => x.ProviderId).Equal(Guid.Parse("4adfdbdd-050e-4a83-b08a-4b6afaa00610"));
        }
    }
}