using Barbershop.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

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

            builder.HasMany(a => a.Services)
                    .WithMany(s => s.Appointments)
                    .UsingEntity<Dictionary<string, object>>(
                        "AppointmentService",
                        j => j
                            .HasOne<Service>()
                            .WithMany()
                            .HasForeignKey("ServiceId")
                            .HasConstraintName("FK_AppointmentService_ServiceId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j => j
                            .HasOne<Appointment>()
                            .WithMany()
                            .HasForeignKey("AppointmentId")
                            .HasConstraintName("FK_AppointmentService_AppointmentId")
                            .OnDelete(DeleteBehavior.ClientCascade),
                        j =>
                        {
                            j.Property<DateTime>("CreatedAt").HasDefaultValueSql("CURRENT_TIMESTAMP");
                            j.Property<DateTime>("UpdatedAt").HasDefaultValueSql("CURRENT_TIMESTAMP");
                            j.HasKey("AppointmentId", "ServiceId");
                        });


            builder.Property(x => x.CreatedAt)
                .HasColumnType("DATETIME");
            builder.Property(x => x.UpdatedAt)
                .HasColumnType("DATETIME");
        }
    }
}
