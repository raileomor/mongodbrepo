using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IAccountServices
    {
        Task<Account> GetByIdAsync(string id);

        Task<Account> GetOneAsync(Expression<Func<Account, bool>> filter);

        Task<IEnumerable<Account>> GetAllAsync();

        Task<Account> AddAsync(Account account);

        Task<bool> ReplaceAsync(string id, Account accountIn);
        
        Task<bool> UpdateProductsAsync(Account accountIn);

        Task<bool> DeleteAsync(Account accountIn);

        Task<bool> DeleteAsync(string id);
    }
}
