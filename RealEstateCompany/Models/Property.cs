using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Million.Api.Models
{
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string IdOwner { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;

        // Usamos Decimal128 para preservar precisión
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
