using Microsoft.AspNetCore.Mvc;
using Million.Api.Repositories;
using Million.Api.Dtos;
using System.Linq;

namespace Million.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyRepository _repo;

        public PropertiesController(IPropertyRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? name, [FromQuery] string? address,
            [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice,
            [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var props = await _repo.GetAsync(name, address, minPrice, maxPrice, page, pageSize);
            var dtos = props.Select(p => new PropertyDto {
                Id = p.Id,
                IdOwner = p.IdOwner,
                Name = p.Name,
                Address = p.Address,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            });
            return Ok(dtos);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return NotFound();
            var dto = new PropertyDto {
                Id = p.Id,
                IdOwner = p.IdOwner,
                Name = p.Name,
                Address = p.Address,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            };
            return Ok(dto);
        }
    }
}
