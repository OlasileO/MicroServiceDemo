using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderWeb.Model
{
    [Serializable, BsonIgnoreExtraElements]
    public class Order
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public int CustomerId { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime OrderOn { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
