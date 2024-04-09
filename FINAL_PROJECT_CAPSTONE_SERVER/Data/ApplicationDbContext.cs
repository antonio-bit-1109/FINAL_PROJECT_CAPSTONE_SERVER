using FINAL_PROJECT_CAPSTONE_SERVER.Models;
using Microsoft.EntityFrameworkCore;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<Utente> Utenti { get; set; }
		public DbSet<Prodotto> Prodotti { get; set; }
		public DbSet<ProdottoVeduto> ProdottiVenduti { get; set; }
		public DbSet<Esercizio> Esercizi { get; set; }

		public DbSet<Allenamento> Allenamenti { get; set; }

		public DbSet<EserciziInAllenamento> EserciziInAllenamenti { get; set; }

		public DbSet<AllenamentoCompletato> AllenamentiCompletati { get; set; }

		public DbSet<Trainer> Trainers { get; set; }

	}

}
