﻿// <auto-generated />
using System;
using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FINAL_PROJECT_CAPSTONE_SERVER.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240325170838_rinominacolonnaPrezzoProdotti_in_tabellaProdottiVenduti")]
    partial class rinominacolonnaPrezzoProdotti_in_tabellaProdottiVenduti
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.Allenamento", b =>
                {
                    b.Property<int>("IdAllenamento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAllenamento"));

                    b.Property<int>("IdUtente")
                        .HasColumnType("int");

                    b.HasKey("IdAllenamento");

                    b.HasIndex("IdUtente");

                    b.ToTable("Allenamenti");
                });

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.EserciziInAllenamento", b =>
                {
                    b.Property<int>("IdEserciziInAllenamento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEserciziInAllenamento"));

                    b.Property<int>("IdAllenamento")
                        .HasColumnType("int");

                    b.Property<int>("IdEsercizio")
                        .HasColumnType("int");

                    b.HasKey("IdEserciziInAllenamento");

                    b.HasIndex("IdAllenamento");

                    b.HasIndex("IdEsercizio");

                    b.ToTable("EserciziInAllenamenti");
                });

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.Esercizio", b =>
                {
                    b.Property<int>("IdEsercizio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEsercizio"));

                    b.Property<string>("DescrizioneEsercizio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Difficolta")
                        .HasColumnType("int");

                    b.Property<string>("ImmagineEsercizio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCardio")
                        .HasColumnType("bit");

                    b.Property<bool>("IsStrenght")
                        .HasColumnType("bit");

                    b.Property<string>("NomeEsercizio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ripetizioni")
                        .HasColumnType("int");

                    b.Property<int>("Serie")
                        .HasColumnType("int");

                    b.Property<int>("TempoRecupero")
                        .HasColumnType("int");

                    b.HasKey("IdEsercizio");

                    b.ToTable("Esercizi");
                });

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.Prodotto", b =>
                {
                    b.Property<int>("IdProdotto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProdotto"));

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImmagineProdotto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeProdotto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PrezzoProdotto")
                        .HasColumnType("float");

                    b.HasKey("IdProdotto");

                    b.ToTable("Prodotti");
                });

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.ProdottoVeduto", b =>
                {
                    b.Property<int>("IdProdottoVeduto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProdottoVeduto"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdProdotto")
                        .HasColumnType("int");

                    b.Property<int>("IdUtente")
                        .HasColumnType("int");

                    b.Property<double>("PrezzoTotTransazione")
                        .HasColumnType("float");

                    b.Property<int>("Quantita")
                        .HasColumnType("int");

                    b.HasKey("IdProdottoVeduto");

                    b.HasIndex("IdProdotto");

                    b.HasIndex("IdUtente");

                    b.ToTable("ProdottiVenduti");
                });

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.StatisticheUtente", b =>
                {
                    b.Property<int>("IdStatisticaUtente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdStatisticaUtente"));

                    b.Property<int>("IdUtente")
                        .HasColumnType("int");

                    b.Property<int>("TotaleRipetizioni")
                        .HasColumnType("int");

                    b.Property<int>("TotaleSerie")
                        .HasColumnType("int");

                    b.HasKey("IdStatisticaUtente");

                    b.HasIndex("IdUtente");

                    b.ToTable("StatisticheUtenti");
                });

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.Utente", b =>
                {
                    b.Property<int>("IdUtente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUtente"));

                    b.Property<string>("Cognome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImmagineProfilo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ruolo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUtente");

                    b.ToTable("Utenti");
                });

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.Allenamento", b =>
                {
                    b.HasOne("FINAL_PROJECT_CAPSTONE_SERVER.Models.Utente", "Utente")
                        .WithMany("Allenamenti")
                        .HasForeignKey("IdUtente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utente");
                });

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.EserciziInAllenamento", b =>
                {
                    b.HasOne("FINAL_PROJECT_CAPSTONE_SERVER.Models.Allenamento", "Allenamento")
                        .WithMany("EserciziInAllenamento")
                        .HasForeignKey("IdAllenamento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FINAL_PROJECT_CAPSTONE_SERVER.Models.Esercizio", "Esercizio")
                        .WithMany("EserciziInAllenamento")
                        .HasForeignKey("IdEsercizio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Allenamento");

                    b.Navigation("Esercizio");
                });

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.ProdottoVeduto", b =>
                {
                    b.HasOne("FINAL_PROJECT_CAPSTONE_SERVER.Models.Prodotto", "Prodotto")
                        .WithMany("ProdottiVenduti")
                        .HasForeignKey("IdProdotto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FINAL_PROJECT_CAPSTONE_SERVER.Models.Utente", "Utente")
                        .WithMany("ProdottiAcquisati")
                        .HasForeignKey("IdUtente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prodotto");

                    b.Navigation("Utente");
                });

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.StatisticheUtente", b =>
                {
                    b.HasOne("FINAL_PROJECT_CAPSTONE_SERVER.Models.Utente", "Utente")
                        .WithMany("StatisticheUtente")
                        .HasForeignKey("IdUtente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utente");
                });

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.Allenamento", b =>
                {
                    b.Navigation("EserciziInAllenamento");
                });

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.Esercizio", b =>
                {
                    b.Navigation("EserciziInAllenamento");
                });

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.Prodotto", b =>
                {
                    b.Navigation("ProdottiVenduti");
                });

            modelBuilder.Entity("FINAL_PROJECT_CAPSTONE_SERVER.Models.Utente", b =>
                {
                    b.Navigation("Allenamenti");

                    b.Navigation("ProdottiAcquisati");

                    b.Navigation("StatisticheUtente");
                });
#pragma warning restore 612, 618
        }
    }
}
