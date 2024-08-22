using Barbershop.API.Data;
using Barbershop.API.Models;
using Barbershop.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Barbershop.API.Controllers
{
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly BarbershopContext _context;

        private readonly IMemoryCache _cache;

        public ServiceController(BarbershopContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet("v1/services")]
        public async Task<ActionResult<IEnumerable<Service>>> Get()
        {
            try
            {

                var services = await _cache.GetOrCreateAsync(
                    "ServicesCache",
                    async cacheEntry =>
                    {
                        cacheEntry.SlidingExpiration = TimeSpan.FromHours(3);
                        return await _context.Services.ToListAsync();
                    }
                ) ?? [];
                return Ok(new ResultViewModel<List<Service>>(services));
            }
            catch (Exception) { }
            {
                return StatusCode(500, new ResultViewModel<Barber>("Internal server error"));
            }
        }

        [HttpGet("v1/services/{id:int}")]
        public async Task<ActionResult<Service>> GetById([FromRoute] int id)
        {
            try
            {
                var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);
                if (service == null) return NotFound(new ResultViewModel<Service>("Service not found"));

                return Ok(new ResultViewModel<Service>(service));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Barber>("Internal server error"));
            }
        }

        [HttpPost("v1/services")]
        public async Task<ActionResult<Service>> Post([FromBody] Service model)
        {
            try
            {
                var service = new Service(model.Name);

                await _context.Services.AddAsync(service);
                await _context.SaveChangesAsync();

                return Created($"v1/services/{service.Id}", new ResultViewModel<Service>(service));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Barber>("Internal server error"));
            }

        }

        [HttpPut("v1/services/{id:int}")]
        public async Task<ActionResult<Service>> Put([FromBody] Service model, [FromRoute] int id)
        {
            try
            {
                var service = _context.Services.FirstOrDefault(x => x.Id == id);
                if (service == null) return NotFound(new ResultViewModel<Service>("Service not found"));

                service.UpdateName(model.Name);

                _context.Update(service);
                await _context.SaveChangesAsync();

                return Ok(new ResultViewModel<Service>(service));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Barber>("Internal server error"));
            }
        }

        [HttpDelete("v1/services/{id:int}")]
        public async Task<ActionResult<Service>> Delete([FromRoute] int id)
        {
            try
            {
                var service = _context.Services.FirstOrDefault(x => x.Id == id);
                if (service == null) return NotFound(new ResultViewModel<Service>("Service not found"));

                _context.Remove(service);
                await _context.SaveChangesAsync();

                return Ok(new ResultViewModel<Service>(service));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Barber>("Internal server error"));
            }

        }
    }
}
