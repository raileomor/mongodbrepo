using FluentValidation;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IAccountProductValidator: IValidator<Account>{}
}