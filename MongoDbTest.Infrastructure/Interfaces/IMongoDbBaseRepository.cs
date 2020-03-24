using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IMongoDbBaseRepository<TDocument> where TDocument : class
    {
        Task<TDocument> CreateAsync(TDocument obj);

        Task<bool> ReplaceAsync(TDocument obj);
        
        Task<bool> UpdateSetAsync(TDocument obj, string name, object value);

        Task<bool> DeleteAsync(string id);

        Task<TDocument> GetAsync(string id);

        Task<IEnumerable<TDocument>> GetAsync();
        
        Task<IEnumerable<TDocument>> GetAsync(TDocument obj);
    }
}
