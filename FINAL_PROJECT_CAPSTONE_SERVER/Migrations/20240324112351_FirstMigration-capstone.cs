using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_PROJECT_CAPSTONE_SERVER.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigrationcapstone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Esercizi",
                columns: table => new
                {
                    IdEsercizio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeEsercizio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImmagineEsercizio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescrizioneEsercizio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Difficolta = table.Column<int>(type: "int", nullable: false),
                    IsCardio = table.Column<bool>(type: "bit", nullable: false),
                    IsStrenght = table.Column<bool>(type: "bit", nullable: false),
                    TempoRecupero = table.Column<int>(type: "int", nullable: false),
                    Serie = table.Column<int>(type: "int", nullable: false),
                    Ripetizioni = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Esercizi", x => x.IdEsercizio);
                });

            migrationBuilder.CreateTable(
                name: "Prodotti",
                columns: table => new
                {
                    IdProdotto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProdotto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezzoProdotto = table.Column<double>(type: "float", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodotti", x => x.IdProdotto);
                });

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    IdUtente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImmagineProfilo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruolo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.IdUtente);
                });

            migrationBuilder.CreateTable(
                name: "Allenamenti",
                columns: table => new
                {
                    IdAllenamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtenteIdUtente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allenamenti", x => x.IdAllenamento);
                    table.ForeignKey(
                        name: "FK_Allenamenti_Utenti_UtenteIdUtente",
                        column: x => x.UtenteIdUtente,
                        principalTable: "Utenti",
                        principalColumn: "IdUtente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdottiVenduti",
                columns: table => new
                {
                    IdProdottoVeduto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProdotto = table.Column<int>(type: "int", nullable: false),
                    IdUtente = table.Column<int>(type: "int", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProdottoIdProdotto = table.Column<int>(type: "int", nullable: false),
                    UtenteIdUtente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdottiVenduti", x => x.IdProdottoVeduto);
                    table.ForeignKey(
                        name: "FK_ProdottiVenduti_Prodotti_ProdottoIdProdotto",
                        column: x => x.ProdottoIdProdotto,
                        principalTable: "Prodotti",
                        principalColumn: "IdProdotto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdottiVenduti_Utenti_UtenteIdUtente",
                        column: x => x.UtenteIdUtente,
                        principalTable: "Utenti",
                        principalColumn: "IdUtente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatisticheUtenti",
                columns: table => new
                {
                    IdStatisticaUtente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtente = table.Column<int>(type: "int", nullable: false),
                    TotaleSerie = table.Column<int>(type: "int", nullable: false),
                    TotaleRipetizioni = table.Column<int>(type: "int", nullable: false),
                    UtenteIdUtente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticheUtenti", x => x.IdStatisticaUtente);
                    table.ForeignKey(
                        name: "FK_StatisticheUtenti_Utenti_UtenteIdUtente",
                        column: x => x.UtenteIdUtente,
                        principalTable: "Utenti",
                        principalColumn: "IdUtente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EserciziInAllenamenti",
                columns: table => new
                {
                    IdEserciziInAllenamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAllenamento = table.Column<int>(type: "int", nullable: false),
                    IdEsercizio = table.Column<int>(type: "int", nullable: false),
                    AllenamentoIdAllenamento = table.Column<int>(type: "int", nullable: false),
                    EsercizioIdEsercizio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EserciziInAllenamenti", x => x.IdEserciziInAllenamento);
                    table.ForeignKey(
                        name: "FK_EserciziInAllenamenti_Allenamenti_AllenamentoIdAllenamento",
                        column: x => x.AllenamentoIdAllenamento,
                        principalTable: "Allenamenti",
                        principalColumn: "IdAllenamento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EserciziInAllenamenti_Esercizi_EsercizioIdEsercizio",
                        column: x => x.EsercizioIdEsercizio,
                        principalTable: "Esercizi",
                        principalColumn: "IdEsercizio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allenamenti_UtenteIdUtente",
                table: "Allenamenti",
                column: "UtenteIdUtente");

            migrationBuilder.CreateIndex(
                name: "IX_EserciziInAllenamenti_AllenamentoIdAllenamento",
                table: "EserciziInAllenamenti",
                column: "AllenamentoIdAllenamento");

            migrationBuilder.CreateIndex(
                name: "IX_EserciziInAllenamenti_EsercizioIdEsercizio",
                table: "EserciziInAllenamenti",
                column: "EsercizioIdEsercizio");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottiVenduti_ProdottoIdProdotto",
                table: "ProdottiVenduti",
                column: "ProdottoIdProdotto");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottiVenduti_UtenteIdUtente",
                table: "ProdottiVenduti",
                column: "UtenteIdUtente");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticheUtenti_UtenteIdUtente",
                table: "StatisticheUtenti",
                column: "UtenteIdUtente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EserciziInAllenamenti");

            migrationBuilder.DropTable(
                name: "ProdottiVenduti");

            migrationBuilder.DropTable(
                name: "StatisticheUtenti");

            migrationBuilder.DropTable(
                name: "Allenamenti");

            migrationBuilder.DropTable(
                name: "Esercizi");

            migrationBuilder.DropTable(
                name: "Prodotti");

            migrationBuilder.DropTable(
                name: "Utenti");
        }
    }
}
