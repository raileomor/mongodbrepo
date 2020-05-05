using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IAccountValidator
    {
        IEnumerable<ValidationResult> Results { get; }

        Task<IEnumerable<ValidationFailure>> ValidateAsync(Account account);
    }
}