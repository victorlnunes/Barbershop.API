using Barbershop.API.Data;
using Barbershop.API.Models;
using Barbershop.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Barbershop.API.Controllers
{
    [ApiController]
    public class BarberController : ControllerBase
    {
        private readonly BarbershopContext _context;

        private readonly IMemoryCache _cache;

        public BarberController(BarbershopContext contex, IMemoryCache cache)
        {
            _context = contex;
            _cache = cache;
        }

        [HttpGet("v1/barbers")]
        public async Task<ActionResult<IEnumerable<Barber>>> Get()
        {
            try
            {
                var barbers = await _cache.GetOrCreateAsync(
                   "ServicesCache",
                   async cacheEntry =>
                   {
                       cacheEntry.SlidingExpiration = TimeSpan.FromHours(3);
                       return await _context.Barbers.ToListAsync();
                   }
               ) ?? [];

                return Ok(new ResultViewModel<List<Barber>>(barbers));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Barber>("Internal server error"));
            }
        }
        [HttpGet("v1/barbers/{id:int}")]
        public async Task<ActionResult<Barber>> GetById([FromRoute] int id)
        {
            try
            {
                var barber = await _context.Barbers.FirstOrDefaultAsync(x => x.Id == id);

                if (barber == null) return NotFound(new ResultViewModel<Barber>("Barber not found"));

                return Ok(new ResultViewModel<Barber>(barber));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Barber>("Internal server error"));
            }
        }

        [HttpPost("v1/barbers")]
        public async Task<ActionResult<Barber>> Post([FromBody] Barber model)
        {
            try
            {
                var barber = new Barber(model.FirstName, model.LastName, model.Email, model.Phone, model.Bio);
                await _context.Barbers.AddAsync(barber);
                await _context.SaveChangesAsync();
                return Created($"v1/barbers/{barber.Id}", new ResultViewModel<Barber>(barber));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("IX_Barber_Email") == true)
                    return Conflict(new ResultViewModel<Client>("Email already registered"));
                else if (ex.InnerException?.Message.Contains("IX_Barber_Phone") == true)
                    return Conflict(new ResultViewModel<Client>("Phone already registered"));
                else throw;
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Barber>("Internal server error"));
            }
        }

        [HttpPut("v1/barbers")]
        public async Task<ActionResult<Barber>> Put([FromBody] Barber model)
        {
            try
            {
                var barber = await _context.Barbers.FirstOrDefaultAsync(x => x.Id == model.Id);

                if (barber == null) return NotFound(new ResultViewModel<Barber>("Barber not found"));

                barber.UpdateName(model.FirstName, model.LastName);
                barber.UpdatePhone(model.Phone);
                barber.UpdateEmail(model.Email);
                barber.UpdateBio(model.Bio);

                _context.Barbers.Update(barber);
                await _context.SaveChangesAsync();

                return Ok(new ResultViewModel<Barber>(barber));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("IX_Barber_Email") == true)
                    return Conflict(new ResultViewModel<Client>("Email already registered"));
                else if (ex.InnerException?.Message.Contains("IX_Barber_Phone") == true)
                    return Conflict(new ResultViewModel<Client>("Phone already registered"));
                else throw;
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Barber>("Internal server error"));
            }
        }

        [HttpDelete("v1/barbers/{id:int}")]
        public async Task<ActionResult<Barber>> Delete([FromRoute] int id)
        {
            try
            {
                var barber = await _context.Barbers.FirstOrDefaultAsync(x => x.Id == id);

                if (barber == null) return NotFound(new ResultViewModel<Barber>("Barber not found"));

                _context.Barbers.Remove(barber);
                await _context.SaveChangesAsync();

                return Ok(new ResultViewModel<Barber>(barber));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Barber>("Internal server error"));
            }
        }

    }
}
