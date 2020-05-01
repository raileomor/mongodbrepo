using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IAccountValidationRule: IValidationRule
    {
        IEnumerable<ValidationResult> Results { get; }
        void Add(IValidator validator);
    }
}