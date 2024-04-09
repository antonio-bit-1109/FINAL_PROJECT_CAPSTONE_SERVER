using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Models
{
	public class EserciziInAllenamento
	{
		[Key]
		public int IdEserciziInAllenamento { get; set; }

		[Required]
		[ForeignKey("Allenamento")]
		public int IdAllenamento { get; set; }

		[Required]
		[ForeignKey("Esercizio")]
		public int IdEsercizio { get; set; }

		virtual public Allenamento Allenamento { get; set; }
		virtual public Esercizio Esercizio { get; set; }
	}
}
