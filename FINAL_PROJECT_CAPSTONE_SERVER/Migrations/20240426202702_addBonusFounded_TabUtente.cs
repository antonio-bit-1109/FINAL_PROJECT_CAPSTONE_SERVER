using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_PROJECT_CAPSTONE_SERVER.Migrations
{
    /// <inheritdoc />
    public partial class addBonusFounded_TabUtente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBonusFounded",
                table: "Utenti",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBonusFounded",
                table: "Utenti");
        }
    }
}
