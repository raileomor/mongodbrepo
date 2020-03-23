using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbTest.Infrastructure.Interfaces;

namespace MongoDbTest.Infrastructure.Models
{
    [Table("accounts")]
    public class Account: IMongodbBaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("account_id")]
        public int? AccountId { get; set; }

        [BsonElement("limit")]
        public int? Limit { get; set; }

        [BsonElement("products")]
        public List<string> Products { get; set; }
    }
}
