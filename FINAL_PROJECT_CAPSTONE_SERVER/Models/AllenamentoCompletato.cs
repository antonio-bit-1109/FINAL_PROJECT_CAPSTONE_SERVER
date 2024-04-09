using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Models
{
	public class AllenamentoCompletato
	{
		[Key]
		public int IdAllenamentoCompletato { get; set; }

		[ForeignKey("Utente")]
		public int IdUtente { get; set; }

		[ForeignKey("Allenamento")]
		public int IdAllenamento { get; set; }

		[Required]
		public DateTime DataEOraDelCompletamento { get; set; } = DateTime.Now;

		public virtual Allenamento Allenamento { get; set; }
		public virtual Utente Utente { get; set; }
	}
}
