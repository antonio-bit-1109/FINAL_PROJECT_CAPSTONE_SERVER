using System.ComponentModel.DataAnnotations;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Models
{
	public class Trainer
	{
		[Key]
		public int IdTrainer { get; set; }

		[Required]
		public string Nome { get; set; }

		[Required]
		public string Cognome { get; set; }

		[Required]
		public string Qualifica { get; set; }

		public string? ImmagineProfilo { get; set; } = "default.jpg";

		public virtual ICollection<Utente> Utenti { get; set; }
	}
}
