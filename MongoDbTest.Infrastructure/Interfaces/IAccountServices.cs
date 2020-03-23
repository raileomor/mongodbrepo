using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IAccountServices
    {
        Task<IEnumerable<Account>> GetAsync();

        Task<Account> GetAsync(string id);

        Task<IEnumerable<Account>> GetAsync(Account accountFilter);

        Task<Account> CreateAsync(Account account);

        Task<bool> ReplaceAsync(string id, Account accountIn);
        
        Task<bool> UpdateSetAsync(Account accountIn, string name, object value);

        Task<bool> DeleteAsync(Account accountIn);

        Task<bool> DeleteAsync(string id);
    }
}
