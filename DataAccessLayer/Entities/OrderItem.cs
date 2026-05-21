using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLayer.Entities
{
    public class OrderItem
    {
        [BsonElement("ProductID")]
        [BsonRepresentation(BsonType.String)]
        public Guid ProductId { get; set; }

        [BsonElement("Quantity")]
        public int Quantity { get; set; }

        [BsonElement("UnitPrice")]
        [BsonRepresentation(BsonType.Double)]
        public decimal UnitPrice { get; set; }

        [BsonElement("TotalPrice")]
        [BsonRepresentation(BsonType.Double)]
        public decimal TotalPrice { get; set; }
    }
}
