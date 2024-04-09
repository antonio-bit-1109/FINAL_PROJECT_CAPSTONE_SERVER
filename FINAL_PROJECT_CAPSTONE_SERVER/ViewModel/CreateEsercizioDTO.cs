namespace FINAL_PROJECT_CAPSTONE_SERVER.ViewModel
{
	public class CreateEsercizioDTO
	{
		public string NomeEsercizio { get; set; }

		//public string ImmagineEsercizio { get; set; }
		public string DescrizioneEsercizio { get; set; }
		public int DifficoltaEsercizio { get; set; }
		public bool IsStrenght { get; set; }

		public int TempoRecupero { get; set; }

		public int SerieEsercizio { get; set; }
		public int RipetizioniEsercizio { get; set; }

		public string ParteCorpoEsercizio { get; set; }
	}
}
