using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_PROJECT_CAPSTONE_SERVER.Migrations
{
    /// <inheritdoc />
    public partial class chiaviEsterne_tabelle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allenamenti_Utenti_UtenteIdUtente",
                table: "Allenamenti");

            migrationBuilder.DropForeignKey(
                name: "FK_EserciziInAllenamenti_Allenamenti_AllenamentoIdAllenamento",
                table: "EserciziInAllenamenti");

            migrationBuilder.DropForeignKey(
                name: "FK_EserciziInAllenamenti_Esercizi_EsercizioIdEsercizio",
                table: "EserciziInAllenamenti");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdottiVenduti_Prodotti_ProdottoIdProdotto",
                table: "ProdottiVenduti");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdottiVenduti_Utenti_UtenteIdUtente",
                table: "ProdottiVenduti");

            migrationBuilder.DropForeignKey(
                name: "FK_StatisticheUtenti_Utenti_UtenteIdUtente",
                table: "StatisticheUtenti");

            migrationBuilder.DropIndex(
                name: "IX_StatisticheUtenti_UtenteIdUtente",
                table: "StatisticheUtenti");

            migrationBuilder.DropIndex(
                name: "IX_ProdottiVenduti_ProdottoIdProdotto",
                table: "ProdottiVenduti");

            migrationBuilder.DropIndex(
                name: "IX_ProdottiVenduti_UtenteIdUtente",
                table: "ProdottiVenduti");

            migrationBuilder.DropIndex(
                name: "IX_EserciziInAllenamenti_AllenamentoIdAllenamento",
                table: "EserciziInAllenamenti");

            migrationBuilder.DropIndex(
                name: "IX_EserciziInAllenamenti_EsercizioIdEsercizio",
                table: "EserciziInAllenamenti");

            migrationBuilder.DropIndex(
                name: "IX_Allenamenti_UtenteIdUtente",
                table: "Allenamenti");

            migrationBuilder.DropColumn(
                name: "UtenteIdUtente",
                table: "StatisticheUtenti");

            migrationBuilder.DropColumn(
                name: "ProdottoIdProdotto",
                table: "ProdottiVenduti");

            migrationBuilder.DropColumn(
                name: "UtenteIdUtente",
                table: "ProdottiVenduti");

            migrationBuilder.DropColumn(
                name: "AllenamentoIdAllenamento",
                table: "EserciziInAllenamenti");

            migrationBuilder.DropColumn(
                name: "EsercizioIdEsercizio",
                table: "EserciziInAllenamenti");

            migrationBuilder.DropColumn(
                name: "UtenteIdUtente",
                table: "Allenamenti");

            migrationBuilder.AlterColumn<int>(
                name: "IdUtente",
                table: "Allenamenti",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticheUtenti_IdUtente",
                table: "StatisticheUtenti",
                column: "IdUtente");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottiVenduti_IdProdotto",
                table: "ProdottiVenduti",
                column: "IdProdotto");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottiVenduti_IdUtente",
                table: "ProdottiVenduti",
                column: "IdUtente");

            migrationBuilder.CreateIndex(
                name: "IX_EserciziInAllenamenti_IdAllenamento",
                table: "EserciziInAllenamenti",
                column: "IdAllenamento");

            migrationBuilder.CreateIndex(
                name: "IX_EserciziInAllenamenti_IdEsercizio",
                table: "EserciziInAllenamenti",
                column: "IdEsercizio");

            migrationBuilder.CreateIndex(
                name: "IX_Allenamenti_IdUtente",
                table: "Allenamenti",
                column: "IdUtente");

            migrationBuilder.AddForeignKey(
                name: "FK_Allenamenti_Utenti_IdUtente",
                table: "Allenamenti",
                column: "IdUtente",
                principalTable: "Utenti",
                principalColumn: "IdUtente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EserciziInAllenamenti_Allenamenti_IdAllenamento",
                table: "EserciziInAllenamenti",
                column: "IdAllenamento",
                principalTable: "Allenamenti",
                principalColumn: "IdAllenamento",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EserciziInAllenamenti_Esercizi_IdEsercizio",
                table: "EserciziInAllenamenti",
                column: "IdEsercizio",
                principalTable: "Esercizi",
                principalColumn: "IdEsercizio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdottiVenduti_Prodotti_IdProdotto",
                table: "ProdottiVenduti",
                column: "IdProdotto",
                principalTable: "Prodotti",
                principalColumn: "IdProdotto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdottiVenduti_Utenti_IdUtente",
                table: "ProdottiVenduti",
                column: "IdUtente",
                principalTable: "Utenti",
                principalColumn: "IdUtente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatisticheUtenti_Utenti_IdUtente",
                table: "StatisticheUtenti",
                column: "IdUtente",
                principalTable: "Utenti",
                principalColumn: "IdUtente",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allenamenti_Utenti_IdUtente",
                table: "Allenamenti");

            migrationBuilder.DropForeignKey(
                name: "FK_EserciziInAllenamenti_Allenamenti_IdAllenamento",
                table: "EserciziInAllenamenti");

            migrationBuilder.DropForeignKey(
                name: "FK_EserciziInAllenamenti_Esercizi_IdEsercizio",
                table: "EserciziInAllenamenti");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdottiVenduti_Prodotti_IdProdotto",
                table: "ProdottiVenduti");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdottiVenduti_Utenti_IdUtente",
                table: "ProdottiVenduti");

            migrationBuilder.DropForeignKey(
                name: "FK_StatisticheUtenti_Utenti_IdUtente",
                table: "StatisticheUtenti");

            migrationBuilder.DropIndex(
                name: "IX_StatisticheUtenti_IdUtente",
                table: "StatisticheUtenti");

            migrationBuilder.DropIndex(
                name: "IX_ProdottiVenduti_IdProdotto",
                table: "ProdottiVenduti");

            migrationBuilder.DropIndex(
                name: "IX_ProdottiVenduti_IdUtente",
                table: "ProdottiVenduti");

            migrationBuilder.DropIndex(
                name: "IX_EserciziInAllenamenti_IdAllenamento",
                table: "EserciziInAllenamenti");

            migrationBuilder.DropIndex(
                name: "IX_EserciziInAllenamenti_IdEsercizio",
                table: "EserciziInAllenamenti");

            migrationBuilder.DropIndex(
                name: "IX_Allenamenti_IdUtente",
                table: "Allenamenti");

            migrationBuilder.AddColumn<int>(
                name: "UtenteIdUtente",
                table: "StatisticheUtenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProdottoIdProdotto",
                table: "ProdottiVenduti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UtenteIdUtente",
                table: "ProdottiVenduti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AllenamentoIdAllenamento",
                table: "EserciziInAllenamenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EsercizioIdEsercizio",
                table: "EserciziInAllenamenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "IdUtente",
                table: "Allenamenti",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UtenteIdUtente",
                table: "Allenamenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StatisticheUtenti_UtenteIdUtente",
                table: "StatisticheUtenti",
                column: "UtenteIdUtente");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottiVenduti_ProdottoIdProdotto",
                table: "ProdottiVenduti",
                column: "ProdottoIdProdotto");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottiVenduti_UtenteIdUtente",
                table: "ProdottiVenduti",
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
                name: "IX_Allenamenti_UtenteIdUtente",
                table: "Allenamenti",
                column: "UtenteIdUtente");

            migrationBuilder.AddForeignKey(
                name: "FK_Allenamenti_Utenti_UtenteIdUtente",
                table: "Allenamenti",
                column: "UtenteIdUtente",
                principalTable: "Utenti",
                principalColumn: "IdUtente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EserciziInAllenamenti_Allenamenti_AllenamentoIdAllenamento",
                table: "EserciziInAllenamenti",
                column: "AllenamentoIdAllenamento",
                principalTable: "Allenamenti",
                principalColumn: "IdAllenamento",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EserciziInAllenamenti_Esercizi_EsercizioIdEsercizio",
                table: "EserciziInAllenamenti",
                column: "EsercizioIdEsercizio",
                principalTable: "Esercizi",
                principalColumn: "IdEsercizio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdottiVenduti_Prodotti_ProdottoIdProdotto",
                table: "ProdottiVenduti",
                column: "ProdottoIdProdotto",
                principalTable: "Prodotti",
                principalColumn: "IdProdotto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdottiVenduti_Utenti_UtenteIdUtente",
                table: "ProdottiVenduti",
                column: "UtenteIdUtente",
                principalTable: "Utenti",
                principalColumn: "IdUtente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatisticheUtenti_Utenti_UtenteIdUtente",
                table: "StatisticheUtenti",
                column: "UtenteIdUtente",
                principalTable: "Utenti",
                principalColumn: "IdUtente",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
