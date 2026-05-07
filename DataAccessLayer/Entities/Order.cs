using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLayer.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid _id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid OrderId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid UserId { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime OrderDate { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public decimal TotalAmount { get; set; }
        public List<OrderItem> Items { get; set; } = [];
    }
}
