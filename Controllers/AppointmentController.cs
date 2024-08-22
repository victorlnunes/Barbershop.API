using Barbershop.API.Data;
using Barbershop.API.Models;
using Barbershop.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barbershop.API.Controllers
{
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly BarbershopContext _context;

        public AppointmentController(BarbershopContext context)
        {
            _context = context;
        }

        [HttpGet("v1/appointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> Get()
        {
            try
            {
                var appointments = await _context.Appointments.ToListAsync();
                return Ok(new ResultViewModel<List<Appointment>>(appointments));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Barber>("Internal server error"));
            }
        }

        [HttpGet("v1/appointments/{id:int}")]
        public async Task<ActionResult<Appointment>> GetById([FromRoute] int id)
        {
            try
            {
                var appointment = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
                if (appointment == null) return NotFound(new ResultViewModel<Appointment>("Appointment not found"));

                return Ok(new ResultViewModel<Appointment>(appointment));

            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Barber>("Internal server error"));
            }
        }

        [HttpPost("v1/appointments:")]
        public async Task<ActionResult<Appointment>> Post([FromBody] Appointment model)
        {
            var appointment = new Appointment(model.Schedule, model.Barber, model.Client);
            try
            {
                await _context.Appointments.AddAsync(appointment);
                await _context.SaveChangesAsync();
                return Created($"v1/appointment/{appointment.Id}", appointment);
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Barber>("Internal server error"));
            }
        }
    }
}