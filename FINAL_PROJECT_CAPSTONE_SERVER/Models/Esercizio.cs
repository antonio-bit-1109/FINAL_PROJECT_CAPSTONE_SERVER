using System.ComponentModel.DataAnnotations;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Models
{
	public class Esercizio
	{
		[Key]
		public int IdEsercizio { get; set; }

		[Required]
		public string NomeEsercizio { get; set; }

		[Required]
		public string ImmagineEsercizio { get; set; }

		[Required]

		public string DescrizioneEsercizio { get; set; } = "gif-default.gif";

		[Required]
		public int Difficolta { get; set; }

		[Required]
		public bool IsStrenght { get; set; }
		[Required]
		public int TempoRecupero { get; set; }

		[Required]
		public int Serie { get; set; }

		[Required]
		public int Ripetizioni { get; set; }

		[Required]
		public string ParteDelCorpoAllenata { get; set; }

		public int MET { get; set; }

		public double DurataSingoloEsercizioInMinuti
		{
			get
			{
				return ((Ripetizioni * 2 * Serie) + (TempoRecupero * (Serie - 1))) / 60;
			}
		}


		public virtual ICollection<EserciziInAllenamento> EserciziInAllenamento { get; set; }
	}
}
