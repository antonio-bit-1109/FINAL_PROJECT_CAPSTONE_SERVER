using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINAL_PROJECT_CAPSTONE_SERVER.Migrations
{
    /// <inheritdoc />
    public partial class ModificaUtente_Abbonamento_campi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripeCustomerId",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "ImmagineAbbonamento",
                table: "Abbonamenti");

            migrationBuilder.DropColumn(
                name: "StripePriceId",
                table: "Abbonamenti");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFineAbbonamento",
                table: "Utenti",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInizioAbbonamento",
                table: "Utenti",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPremium",
                table: "Utenti",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFineAbbonamento",
                table: "Abbonamenti",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInizioAbbonamento",
                table: "Abbonamenti",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Abbonamenti",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFineAbbonamento",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "DataInizioAbbonamento",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "IsPremium",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "DataFineAbbonamento",
                table: "Abbonamenti");

            migrationBuilder.DropColumn(
                name: "DataInizioAbbonamento",
                table: "Abbonamenti");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Abbonamenti");

            migrationBuilder.AddColumn<string>(
                name: "StripeCustomerId",
                table: "Utenti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImmagineAbbonamento",
                table: "Abbonamenti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripePriceId",
                table: "Abbonamenti",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
