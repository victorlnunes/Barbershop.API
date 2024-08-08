using Barbershop.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbershop.API.Data.Mappings
{
    public class BarberMap : IEntityTypeConfiguration<Barber>
    {
        public void Configure(EntityTypeBuilder<Barber> builder)
        {
            builder.ToTable("Barber");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20);
            builder.Property(x => x.LastName)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);
            builder.Property(x => x.Phone)
                   .IsRequired()
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(14);
            builder.Property(x => x.Bio)
                    .IsRequired(false);
            builder.Property(x => x.CreatedAt)
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.UpdatedAt)
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasIndex(x => x.Email, "IX_Client_Email").IsUnique();
            builder.HasIndex(x => x.Phone, "IX_Client_Phone").IsUnique();
        }
    }
}
