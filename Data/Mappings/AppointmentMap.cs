using Barbershop.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbershop.API.Data.Mappings
{
    public class AppointmentMap : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointment");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Schedule)
                .IsRequired()
                .HasColumnType("DATETIME");

            builder.HasOne(x => x.Barber)
                .WithMany(x => x.Appointments)
                .HasConstraintName("FK_Appointments_Barber");

            builder.HasOne(x => x.Client)
                .WithMany(x => x.Appointments)
                .HasConstraintName("FK_Appointments_Client");


            builder.Property(x => x.CreatedAt)
                .HasColumnType("DATETIME");
            builder.Property(x => x.UpdatedAt)
                .HasColumnType("DATETIME");
        }
    }
}
