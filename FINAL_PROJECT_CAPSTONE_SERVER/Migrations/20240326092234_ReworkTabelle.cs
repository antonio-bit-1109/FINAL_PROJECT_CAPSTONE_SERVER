using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_PROJECT_CAPSTONE_SERVER.Migrations
{
    /// <inheritdoc />
    public partial class ReworkTabelle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allenamenti_Utenti_IdUtente",
                table: "Allenamenti");

            migrationBuilder.DropTable(
                name: "StatisticheUtenti");

            migrationBuilder.DropIndex(
                name: "IX_Allenamenti_IdUtente",
                table: "Allenamenti");

            migrationBuilder.DropColumn(
                name: "IdUtente",
                table: "Allenamenti");

            migrationBuilder.AddColumn<int>(
                name: "IdTrainer",
                table: "Utenti",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeAllenamento",
                table: "Allenamenti",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AllenamentiCompletati",
                columns: table => new
                {
                    IdAllenamentoCompletato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtente = table.Column<int>(type: "int", nullable: false),
                    IdAllenamento = table.Column<int>(type: "int", nullable: false),
                    DataEOraDelCompletamento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllenamentiCompletati", x => x.IdAllenamentoCompletato);
                    table.ForeignKey(
                        name: "FK_AllenamentiCompletati_Allenamenti_IdAllenamento",
                        column: x => x.IdAllenamento,
                        principalTable: "Allenamenti",
                        principalColumn: "IdAllenamento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllenamentiCompletati_Utenti_IdUtente",
                        column: x => x.IdUtente,
                        principalTable: "Utenti",
                        principalColumn: "IdUtente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    IdTrainer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qualifica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImmagineProfilo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.IdTrainer);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Utenti_IdTrainer",
                table: "Utenti",
                column: "IdTrainer");

            migrationBuilder.CreateIndex(
                name: "IX_AllenamentiCompletati_IdAllenamento",
                table: "AllenamentiCompletati",
                column: "IdAllenamento");

            migrationBuilder.CreateIndex(
                name: "IX_AllenamentiCompletati_IdUtente",
                table: "AllenamentiCompletati",
                column: "IdUtente");

            migrationBuilder.AddForeignKey(
                name: "FK_Utenti_Trainers_IdTrainer",
                table: "Utenti",
                column: "IdTrainer",
                principalTable: "Trainers",
                principalColumn: "IdTrainer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utenti_Trainers_IdTrainer",
                table: "Utenti");

            migrationBuilder.DropTable(
                name: "AllenamentiCompletati");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_Utenti_IdTrainer",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "IdTrainer",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "NomeAllenamento",
                table: "Allenamenti");

            migrationBuilder.AddColumn<int>(
                name: "IdUtente",
                table: "Allenamenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StatisticheUtenti",
                columns: table => new
                {
                    IdStatisticaUtente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtente = table.Column<int>(type: "int", nullable: false),
                    TotaleRipetizioni = table.Column<int>(type: "int", nullable: false),
                    TotaleSerie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticheUtenti", x => x.IdStatisticaUtente);
                    table.ForeignKey(
                        name: "FK_StatisticheUtenti_Utenti_IdUtente",
                        column: x => x.IdUtente,
                        principalTable: "Utenti",
                        principalColumn: "IdUtente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allenamenti_IdUtente",
                table: "Allenamenti",
                column: "IdUtente");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticheUtenti_IdUtente",
                table: "StatisticheUtenti",
                column: "IdUtente");

            migrationBuilder.AddForeignKey(
                name: "FK_Allenamenti_Utenti_IdUtente",
                table: "Allenamenti",
                column: "IdUtente",
                principalTable: "Utenti",
                principalColumn: "IdUtente",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
