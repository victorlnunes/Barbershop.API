using Barbershop.API.Data.Mappings;
using Barbershop.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Barbershop.API.Data
{
    public class BarbershopContext : DbContext
    {
        public BarbershopContext(DbContextOptions<BarbershopContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Client> Clients { get; set; }  
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppointmentMap());
            modelBuilder.ApplyConfiguration(new BarberMap());
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new ServiceMap());  
        }   
    }
}
