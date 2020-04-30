using FluentValidation;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IAccountValidator: IValidator<Account>{}
}