using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_PROJECT_CAPSTONE_SERVER.Migrations
{
    /// <inheritdoc />
    public partial class FixTabellaTrainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cognome",
                table: "Trainers");

            migrationBuilder.RenameColumn(
                name: "Qualifica",
                table: "Trainers",
                newName: "NomePiano");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Trainers",
                newName: "DescrizionePiano");

            migrationBuilder.AddColumn<double>(
                name: "PrezzoPiano",
                table: "Trainers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "StripePriceId",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrezzoPiano",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "StripePriceId",
                table: "Trainers");

            migrationBuilder.RenameColumn(
                name: "NomePiano",
                table: "Trainers",
                newName: "Qualifica");

            migrationBuilder.RenameColumn(
                name: "DescrizionePiano",
                table: "Trainers",
                newName: "Nome");

            migrationBuilder.AddColumn<string>(
                name: "Cognome",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
