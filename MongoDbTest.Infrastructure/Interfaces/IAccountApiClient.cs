using System.Threading.Tasks;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IAccountApiClient
    {
        Task<bool> ExistAsync(string id);
    }
}