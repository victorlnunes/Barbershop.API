using Barbershop.API.Data;
using Barbershop.API.Models;
using Barbershop.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Barbershop.API.Controllers
{
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly BarbershopContext _context;
        public ClientController(BarbershopContext context)
        {
            _context = context;
        }

        [HttpGet("v1/clients")]
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            try
            {
                var clients = await _context.Clients.ToListAsync();
                return Ok(new ResultViewModel<List<Client>>(clients));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Client>("Internal server error"));
            }
        }

        [HttpGet("v1/clients/{id:int}")]
        public async Task<ActionResult<Client>> GetById([FromRoute] int id)
        {
            try
            {
                var client = await _context.Clients.SingleOrDefaultAsync(x => x.Id == id);

                if (client == null) return NotFound(new ResultViewModel<Client>("Client not found"));

                return Ok(new ResultViewModel<Client>(client));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Client>("Internal server error"));
            }
        }

        [HttpPost("v1/clients")]
        public async Task<ActionResult<Client>> Post([FromBody] Client model)
        {
            var client = new Client(model.FirstName, model.LastName, model.Email, model.Phone);
            try
            {
                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();
                return Created($"v1/clients/{client.Id}", new ResultViewModel<Client>(client));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("IX_Client_Email") == true)
                    return Conflict(new ResultViewModel<Client>("Email already registered"));
                else if (ex.InnerException?.Message.Contains("IX_Client_Phone") == true)
                    return Conflict(new ResultViewModel<Client>("Phone already registered"));
                else throw;
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Client>("Internal server error"));
            }
        }

        [HttpPut("v1/clients/{id:int}")]
        public async Task<ActionResult<Client>> Put([FromRoute] int id, [FromBody] Client model)
        {
            try
            {
                var client = await _context.Clients.SingleOrDefaultAsync(x => x.Id == id);

                if (client == null) return NotFound(new ResultViewModel<Client>("Client not found"));

                client.UpdateName(model.FirstName, model.LastName);
                client.UpdateEmail(model.Email);
                client.UpdatePhone(model.Phone);

                _context.Update(client);
                await _context.SaveChangesAsync();

                return Ok(new ResultViewModel<Client>(client));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("IX_Client_Email") == true)
                    return Conflict(new ResultViewModel<Client>("Email already registered"));
                else if (ex.InnerException?.Message.Contains("IX_Client_Phone") == true)
                    return Conflict(new ResultViewModel<Client>("Phone already registered"));
                else throw;
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Client>("Internal server error"));
            }
        }

        [HttpDelete("v1/clients/{id:int}")]
        public async Task<ActionResult<Client>> Delete([FromRoute] int id)
        {
            try
            {
                var client = await _context.Clients.SingleOrDefaultAsync(x => x.Id == id);

                if (client == null) return NotFound(new ResultViewModel<Client>("Client not found"));

                _context.Remove(client);
                await _context.SaveChangesAsync();

                return Ok(new ResultViewModel<Client>(client));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Client>("Internal server error"));
            }
        }
    }
}
