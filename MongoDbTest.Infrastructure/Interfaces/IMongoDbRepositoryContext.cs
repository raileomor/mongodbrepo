using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IMongoDbRepositoryContext
    {
        IMongoCollection<T> DbSet<T>() where T : IMongodbBaseModel;
    }
}
