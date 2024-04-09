using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_PROJECT_CAPSTONE_SERVER.Migrations
{
    /// <inheritdoc />
    public partial class campi_allenamento_noncalcolati : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DurataTotaleAllenamento",
                table: "Allenamenti",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TotaleRipetizioni",
                table: "Allenamenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotaleSerie",
                table: "Allenamenti",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurataTotaleAllenamento",
                table: "Allenamenti");

            migrationBuilder.DropColumn(
                name: "TotaleRipetizioni",
                table: "Allenamenti");

            migrationBuilder.DropColumn(
                name: "TotaleSerie",
                table: "Allenamenti");
        }
    }
}
