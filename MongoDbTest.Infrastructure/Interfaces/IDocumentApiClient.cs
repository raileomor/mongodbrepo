using System.Threading.Tasks;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IDocumentApiClient
    {
        Task<bool> ExistAsync(string id);
    }
}