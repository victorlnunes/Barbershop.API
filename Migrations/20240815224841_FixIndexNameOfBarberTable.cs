using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbershop.API.Migrations
{
    /// <inheritdoc />
    public partial class FixIndexNameOfBarberTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Client_Phone",
                table: "Barber",
                newName: "IX_Barber_Phone");

            migrationBuilder.RenameIndex(
                name: "IX_Client_Email",
                table: "Barber",
                newName: "IX_Barber_Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Barber_Phone",
                table: "Barber",
                newName: "IX_Client_Phone");

            migrationBuilder.RenameIndex(
                name: "IX_Barber_Email",
                table: "Barber",
                newName: "IX_Client_Email");
        }
    }
}
