using System;
using System.Collections.Generic;
using System.Text;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Interfaces
{
    public interface IMongoDbAccountRepository : IMongoDbBaseRepository<Account>
    {
    }
}
