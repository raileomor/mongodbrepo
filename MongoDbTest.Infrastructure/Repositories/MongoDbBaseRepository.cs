using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbTest.Infrastructure.Interfaces;

namespace MongoDbTest.Infrastructure.Repositories
{
    public abstract class MongoDbBaseRepository<TDocument> : IMongoDbBaseRepository<TDocument> where TDocument: IMongodbBaseModel
    {
        private readonly IMongoDbRepositoryContext _mongoContext;
        public IMongoCollection<TDocument> Collection { get; private set; }

        protected MongoDbBaseRepository(IMongoDbRepositoryContext context)
        {
            _mongoContext = context;
            Collection = _mongoContext.GetCollection<TDocument>();
        }

        #region Get

        public async Task<IEnumerable<TDocument>> GetAllAsync()
        {
            var all = await Collection.FindAsync(Builders<TDocument>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task<TDocument> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);

            FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq("_id", objectId);

            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<TDocument> GetOneAsync(Expression<Func<TDocument, bool>> filter)
        {
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        #endregion

        #region Create

        public async Task<TDocument> InsertOneAsync(TDocument obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(TDocument).Name + " object is null");
            }

            await Collection.InsertOneAsync(obj);
            
            return obj;
        }

        #endregion

        #region Update

        public async Task<bool> ReplaceOneAsync(TDocument obj)
        {
            if (!ObjectId.TryParse(obj.Id, out var objectId))
            {
                return false;
            }

            var filter = Builders<TDocument>.Filter.Eq("_id", objectId);
            var updated = await Collection.ReplaceOneAsync(filter, obj);

            return updated != null && updated.IsAcknowledged && updated.ModifiedCount != 0;
        }

        public async Task<bool> UpdateSetAsync(TDocument obj, string name, object value)
        {
            if (!ObjectId.TryParse(obj.Id, out var objectId))
            {
                return false;
            }

            var upsertAdd = Builders<TDocument>.Update.Set(name, value);
            var filter = Builders<TDocument>.Filter.Eq("_id", objectId);
            var updated = await Collection.UpdateOneAsync(filter, upsertAdd);

            return updated != null && updated.IsAcknowledged && updated.ModifiedCount != 0;
        }

        #endregion

        #region Delete

        public async Task<bool> DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return false;
            }

            var deleted = await Collection.DeleteOneAsync(Builders<TDocument>.Filter.Eq("_id", objectId));

            return deleted != null;
        }

        #endregion
    }
}
