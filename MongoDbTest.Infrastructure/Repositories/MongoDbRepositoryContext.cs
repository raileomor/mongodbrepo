using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;
using MongoDbTest.Infrastructure.Repositories.Configurations;

namespace MongoDbTest.Infrastructure.Repositories
{
    public class MongoDbRepositoryContext : IMongoDbRepositoryContext
    {
        private readonly MongoDbConfiguration _mongoDbConfiguration;
        private MongoClient _client { get; set; }
        private IMongoDatabase _db { get; set; }
        public IClientSessionHandle Session { get; set; }

        public MongoDbRepositoryContext(IOptions<MongoDbConfiguration> mongoDbConfiguration)
        {
            _mongoDbConfiguration = mongoDbConfiguration.Value;
            OpenConnection();
        }

        public IMongoDatabase OpenConnection()
        {
            var databaseSettings = new MongoDatabaseSettings
            {
                GuidRepresentation = GuidRepresentation.Standard
            };

            _client = new MongoClient(_mongoDbConfiguration.ConnectionString);
            _db = _client.GetDatabase(_mongoDbConfiguration.DatabaseName, databaseSettings);

            return _db;
        }

        public IMongoCollection<T> DbSet<T>() where T: IMongodbBaseModel
        {
            var collection = typeof(T).GetCustomAttribute<TableAttribute>(false).Name;
            return _db.GetCollection<T>(collection);
        }
    }
}
