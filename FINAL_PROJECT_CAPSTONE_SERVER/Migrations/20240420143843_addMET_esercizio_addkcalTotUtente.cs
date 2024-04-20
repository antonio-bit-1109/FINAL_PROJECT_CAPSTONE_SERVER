using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_PROJECT_CAPSTONE_SERVER.Migrations
{
    /// <inheritdoc />
    public partial class addMET_esercizio_addkcalTotUtente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotaleKcalConsumate",
                table: "Utenti",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MET",
                table: "Esercizi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotaleKcalConsumate",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "MET",
                table: "Esercizi");
        }
    }
}
