using System.ComponentModel.DataAnnotations;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Models
{
	public class Prodotto
	{
		[Key]
		public int IdProdotto { get; set; }

		[Required]
		public string NomeProdotto { get; set; }

		[Required]
		public double PrezzoProdotto { get; set; }

		[Required]
		public string Descrizione { get; set; }

		[Required]
		public string ImmagineProdotto { get; set; } = "prodotto-default.jpg";

		public virtual ICollection<ProdottoVeduto> ProdottiVenduti { get; set; }
	}
}
