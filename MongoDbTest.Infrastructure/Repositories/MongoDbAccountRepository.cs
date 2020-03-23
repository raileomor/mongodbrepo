using System;
using System.Collections.Generic;
using System.Text;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Repositories
{
    public class MongoDbAccountRepository : MongoDbBaseRepository<Account>, IMongoDbAccountRepository
    {
        public MongoDbAccountRepository(IMongoDbRepositoryContext context) : base(context)
        {
        }
    }
}
