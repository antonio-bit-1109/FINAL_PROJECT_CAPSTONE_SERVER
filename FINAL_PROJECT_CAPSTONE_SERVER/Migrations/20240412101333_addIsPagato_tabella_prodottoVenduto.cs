using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_PROJECT_CAPSTONE_SERVER.Migrations
{
    /// <inheritdoc />
    public partial class addIsPagato_tabella_prodottoVenduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPagato",
                table: "ProdottiVenduti",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPagato",
                table: "ProdottiVenduti");
        }
    }
}
