using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IMongoDbRepositoryContext
    {
        IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : IMongodbBaseModel;
    }
}
