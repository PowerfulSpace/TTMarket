using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TTMarket.Products.Domain.Common
{
    public interface IDocument
    {
        [BsonId]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}