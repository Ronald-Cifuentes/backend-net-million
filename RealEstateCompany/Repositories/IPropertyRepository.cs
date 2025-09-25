using Million.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Api.Repositories
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice, int page, int pageSize);
        Task<Property?> GetByIdAsync(string id);
        Task CreateManyAsync(IEnumerable<Property> properties);
    }
}
