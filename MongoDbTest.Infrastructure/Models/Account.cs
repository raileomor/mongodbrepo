using System;
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

        [BsonElement("provider_id")]
        public Guid ProviderId { get; set; }

        [BsonElement("account_id")]
        public int? AccountId { get; set; }

        [BsonElement("limit")]
        [BsonIgnoreIfNull]
        public int? Limit { get; set; }

        [BsonElement("products")]
        [BsonIgnoreIfNull]
        public List<string> Products { get; set; }

        [BsonIgnore]
        public string Error { get; set;}
    }
}
