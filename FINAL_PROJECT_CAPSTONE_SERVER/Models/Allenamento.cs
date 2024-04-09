using System.ComponentModel.DataAnnotations;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Models
{
	public class Allenamento
	{
		[Key]
		public int IdAllenamento { get; set; }

		[Required]
		public string NomeAllenamento { get; set; }

		[Required]
		public double DurataTotaleAllenamento { get; set; }


		[Required]
		public int TotaleSerie { get; set; }

		[Required]
		public int TotaleRipetizioni { get; set; }


		//[Required]
		//[ForeignKey("Utente")]
		//public int IdUtente { get; set; }
		public virtual ICollection<EserciziInAllenamento> EserciziInAllenamento { get; set; }
		public virtual ICollection<AllenamentoCompletato> AllenamentiCompletati { get; set; }
		//public virtual Utente Utente { get; set; }
	}
}
