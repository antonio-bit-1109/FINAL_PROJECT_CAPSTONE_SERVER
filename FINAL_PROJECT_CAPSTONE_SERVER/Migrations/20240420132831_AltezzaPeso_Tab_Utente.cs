using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_PROJECT_CAPSTONE_SERVER.Migrations
{
    /// <inheritdoc />
    public partial class AltezzaPeso_Tab_Utente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Altezza",
                table: "Utenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Peso",
                table: "Utenti",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Altezza",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "Peso",
                table: "Utenti");
        }
    }
}
