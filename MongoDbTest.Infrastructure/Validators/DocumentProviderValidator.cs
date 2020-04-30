using System;
using FluentValidation;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Validators
{
    public class DocumentProviderValidator: AbstractValidator<Account>
    {
        public DocumentProviderValidator()
        {
            RuleFor(x => x.ProviderId).Equal(Guid.Parse("9089e103-3f0f-41e0-a0f9-c1d1d870c27a"));
        }
    }
}