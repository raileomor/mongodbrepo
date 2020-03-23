using System;
using MongoDB.Bson;

namespace MongoDbTest.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new BsonDocument
            {
                {"a", 1},
                {
                    "b", new BsonArray
                    {
                        new BsonDocument
                        {
                            {"c", 1}
                        }
                    }
                }
            };
        }
    }
}
