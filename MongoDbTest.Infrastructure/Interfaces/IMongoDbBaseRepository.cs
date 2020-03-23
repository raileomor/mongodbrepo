using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IMongoDbBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity obj);

        Task<bool> ReplaceAsync(TEntity obj);
        
        Task<bool> UpdateSetAsync(TEntity obj, string name, object value);

        Task<bool> DeleteAsync(string id);

        Task<TEntity> GetAsync(string id);

        Task<IEnumerable<TEntity>> GetAsync();
        
        Task<IEnumerable<TEntity>> GetAsync(TEntity obj);
    }
}
