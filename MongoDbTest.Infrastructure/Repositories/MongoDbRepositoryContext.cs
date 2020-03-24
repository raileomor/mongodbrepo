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
        public IMongoClient Client { get; protected set; }
        public IMongoDatabase Database { get; protected set; }

        public IClientSessionHandle Session { get; set; }

        public MongoDbRepositoryContext(IOptions<MongoDbConfiguration> mongoDbConfiguration)
        {
            var mongoDbConfig = mongoDbConfiguration.Value;

            var databaseSettings = new MongoDatabaseSettings
            {
                GuidRepresentation = GuidRepresentation.Standard
            };

            Client = new MongoClient(mongoDbConfig.ConnectionString);
            Database = Client.GetDatabase(mongoDbConfig.DatabaseName, databaseSettings);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument: IMongodbBaseModel
        {
            var collection = typeof(TDocument).GetCustomAttribute<TableAttribute>(false).Name;
            return Database.GetCollection<TDocument>(collection);
        }
    }
}
