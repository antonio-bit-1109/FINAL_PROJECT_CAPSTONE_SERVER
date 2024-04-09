namespace FINAL_PROJECT_CAPSTONE_SERVER.ViewModel
{
	public class AllenamentoDto
	{
		public string NomeAllenamento { get; set; }

		public int DurataTotaleAllenamento { get; set; }
		public int SerieTotali { get; set; }
		public int RipetizioniTotali { get; set; }

		public List<int> IdEsercizi { get; set; }
	}

}
