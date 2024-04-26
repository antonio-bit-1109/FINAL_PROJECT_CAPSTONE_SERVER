using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Models
{
	public class Utente
	{
		[Key]
		public int IdUtente { get; set; }

		[Required]
		public string Nome { get; set; }

		[Required]
		public string Cognome { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public int Altezza { get; set; }

		[Required]
		public double Peso { get; set; }

		[Required]
		public string? ImmagineProfilo { get; set; } = "default.jpg";

		[Required]
		public string Ruolo { get; set; } = "utente";

		[ForeignKey("Abbonamento")]
		public int? IdAbbonamento { get; set; }

		public string Email { get; set; }

		//public string? StripeCustomerId { get; set; }

		public bool IsPremium { get; set; } = false;

		public bool IsBonusFounded { get; set; } = false;

		public DateTime? DataInizioAbbonamento { get; set; }
		public DateTime? DataFineAbbonamento { get; set; }

		public double TotaleKcalConsumate { get; set; } = 0;

		public virtual Abbonamento Abbonamento { get; set; }

		//public virtual ICollection<Allenamento> Allenamenti { get; set; }
		public virtual ICollection<ProdottoVeduto> ProdottiAcquisati { get; set; }
		//public virtual ICollection<StatisticheUtente> StatisticheUtente { get; set; }

		public virtual ICollection<AllenamentoCompletato> AllenamentiCompletati { get; set; }
	}
}
