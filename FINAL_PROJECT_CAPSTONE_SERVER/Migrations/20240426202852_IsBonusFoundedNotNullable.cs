using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_PROJECT_CAPSTONE_SERVER.Migrations
{
    /// <inheritdoc />
    public partial class IsBonusFoundedNotNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsBonusFounded",
                table: "Utenti",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsBonusFounded",
                table: "Utenti",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
