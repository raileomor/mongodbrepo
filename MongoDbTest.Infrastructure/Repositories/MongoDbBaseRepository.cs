using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbTest.Infrastructure.Interfaces;

namespace MongoDbTest.Infrastructure.Repositories
{
    public abstract class MongoDbBaseRepository<TEntity> : IMongoDbBaseRepository<TEntity> where TEntity : class, IMongodbBaseModel
    {
        private readonly IMongoDbRepositoryContext _mongoContext;
        public IMongoCollection<TEntity> Collection { get; private set; }

        protected MongoDbBaseRepository(IMongoDbRepositoryContext context)
        {
            _mongoContext = context;
            Collection = _mongoContext.DbSet<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            }

            Collection = _mongoContext.DbSet<TEntity>();
            await Collection.InsertOneAsync(obj);
            
            return obj;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return false;
            }
            
            var deleted = await Collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId));

            return deleted != null;
        }

        public async Task<bool> ReplaceAsync(TEntity obj)
        {
            if (!ObjectId.TryParse(obj.Id, out var objectId))
            {
                return false;
            }

            var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            var updated = await Collection.ReplaceOneAsync(filter, obj);

            return updated != null && updated.IsAcknowledged && updated.ModifiedCount != 0;
        }

        public async Task<bool> UpdateSetAsync(TEntity obj, string name, object value)
        {
            if (!ObjectId.TryParse(obj.Id, out var objectId))
            {
                return false;
            }

            var upsertAdd = Builders<TEntity>.Update.Set(name, value);
            var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            var updated = await Collection.UpdateOneAsync(filter, upsertAdd);

            return updated != null && updated.IsAcknowledged && updated.ModifiedCount != 0;
        }

        public async Task<TEntity> GetAsync(string id)
        {
            var objectId = new ObjectId(id);

            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", objectId);

            Collection = _mongoContext.DbSet<TEntity>();

            return await Collection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            var all = await Collection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }
        
        public async Task<IEnumerable<TEntity>> GetAsync(TEntity obj)
        {
            var all = await Collection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }
    }
}
