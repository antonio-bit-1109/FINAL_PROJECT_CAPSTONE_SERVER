using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_PROJECT_CAPSTONE_SERVER.Migrations
{
    /// <inheritdoc />
    public partial class rinominaTrainerInAbbonamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utenti_Trainers_IdTrainer",
                table: "Utenti");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.RenameColumn(
                name: "IdTrainer",
                table: "Utenti",
                newName: "IdAbbonamento");

            migrationBuilder.RenameIndex(
                name: "IX_Utenti_IdTrainer",
                table: "Utenti",
                newName: "IX_Utenti_IdAbbonamento");

            migrationBuilder.CreateTable(
                name: "Abbonamenti",
                columns: table => new
                {
                    IdAbbonamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeAbbonamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescrizioneAbbonamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezzoAbbonamento = table.Column<double>(type: "float", nullable: false),
                    StripePriceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImmagineAbbonamento = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abbonamenti", x => x.IdAbbonamento);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Utenti_Abbonamenti_IdAbbonamento",
                table: "Utenti",
                column: "IdAbbonamento",
                principalTable: "Abbonamenti",
                principalColumn: "IdAbbonamento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utenti_Abbonamenti_IdAbbonamento",
                table: "Utenti");

            migrationBuilder.DropTable(
                name: "Abbonamenti");

            migrationBuilder.RenameColumn(
                name: "IdAbbonamento",
                table: "Utenti",
                newName: "IdTrainer");

            migrationBuilder.RenameIndex(
                name: "IX_Utenti_IdAbbonamento",
                table: "Utenti",
                newName: "IX_Utenti_IdTrainer");

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    IdTrainer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescrizionePiano = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Immagine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomePiano = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezzoPiano = table.Column<double>(type: "float", nullable: false),
                    StripePriceId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.IdTrainer);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Utenti_Trainers_IdTrainer",
                table: "Utenti",
                column: "IdTrainer",
                principalTable: "Trainers",
                principalColumn: "IdTrainer");
        }
    }
}
