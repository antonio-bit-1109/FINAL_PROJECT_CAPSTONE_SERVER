using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_PROJECT_CAPSTONE_SERVER.Migrations
{
    /// <inheritdoc />
    public partial class rinominacolonnaPrezzoProdotti_in_tabellaProdottiVenduti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrezzoProdotti",
                table: "ProdottiVenduti",
                newName: "PrezzoTotTransazione");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrezzoTotTransazione",
                table: "ProdottiVenduti",
                newName: "PrezzoProdotti");
        }
    }
}
