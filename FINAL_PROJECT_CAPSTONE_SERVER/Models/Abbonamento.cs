using System.ComponentModel.DataAnnotations;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Models
{

	public class Abbonamento
	{
		[Key]
		public int IdAbbonamento { get; set; }

		[Required]
		public string NomeAbbonamento { get; set; }

		[Required]
		public string DescrizioneAbbonamento { get; set; }

		[Required]
		public double PrezzoAbbonamento { get; set; }

		public string? StripePriceId { get; set; }

		public string? ImmagineAbbonamento { get; set; } = "default.jpg";

		public virtual ICollection<Utente> Utenti { get; set; }
	}
}
