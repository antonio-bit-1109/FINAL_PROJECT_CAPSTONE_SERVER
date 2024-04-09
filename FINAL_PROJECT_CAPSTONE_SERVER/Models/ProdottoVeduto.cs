using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Models
{
	public class ProdottoVeduto
	{
		[Key]
		public int IdProdottoVeduto { get; set; }

		[Required]
		[ForeignKey("Prodotto")]
		public int IdProdotto { get; set; }

		[Required]
		[ForeignKey("Utente")]
		public int IdUtente { get; set; }

		[Required]
		public int Quantita { get; set; }
		[Required]
		public DateTime Data { get; set; }

		[Required]
		public double PrezzoTotTransazione { get; set; }

		public virtual Prodotto Prodotto { get; set; }
		public virtual Utente Utente { get; set; }
	}
}
