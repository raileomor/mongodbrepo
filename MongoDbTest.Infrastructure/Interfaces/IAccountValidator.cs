using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IAccountValidator: IValidator<Account>
    {
        IEnumerable<ValidationResult> Results { get; }
    }
}