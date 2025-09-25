using MongoDB.Driver;
using Million.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;

namespace Million.Api.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IMongoCollection<Property> _collection;

        public PropertyRepository(IMongoClient client, IConfiguration config)
        {
            var dbName = config["MongoSettings:DatabaseName"] ?? "milliondb";
            var db = client.GetDatabase(dbName);
            _collection = db.GetCollection<Property>("properties");
        }

        public async Task<IEnumerable<Property>> GetAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice, int page, int pageSize)
        {
            var builder = Builders<Property>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(name))
                filter &= builder.Regex(p => p.Name, new BsonRegularExpression(name, "i"));

            if (!string.IsNullOrWhiteSpace(address))
                filter &= builder.Regex(p => p.Address, new BsonRegularExpression(address, "i"));

            if (minPrice.HasValue)
                filter &= builder.Gte(p => p.Price, minPrice.Value);

            if (maxPrice.HasValue)
                filter &= builder.Lte(p => p.Price, maxPrice.Value);

            var skip = (page - 1) * pageSize;
            return await _collection.Find(filter).Skip(skip).Limit(pageSize).ToListAsync();
        }

        public async Task<Property?> GetByIdAsync(string id)
        {
            var filter = Builders<Property>.Filter.Eq(p => p.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateManyAsync(IEnumerable<Property> properties)
        {
            await _collection.InsertManyAsync(properties);
        }
    }
}
