using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IMongoDbBaseRepository<TDocument>
    {
        Task<TDocument> GetByIdAsync(string id);

        Task<IEnumerable<TDocument>> GetAllAsync();

        Task<TDocument> GetOneAsync(Expression<Func<TDocument, bool>> filter);

        Task<TDocument> InsertOneAsync(TDocument obj);

        Task<bool> ReplaceOneAsync(TDocument obj);
        
        Task<bool> UpdateSetAsync(TDocument obj, string name, object value);

        Task<bool> DeleteAsync(string id);
    }
}
