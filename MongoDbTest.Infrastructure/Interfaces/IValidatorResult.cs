using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MongoDbTest.Infrastructure.Models;
using MongoDbTest.Infrastructure.Validators;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IValidatorResult
    {
        bool IsValid { get; }
        IEnumerable<ValidatorError> Errors { get; }
        object ServiceResult { get; }
    }
}