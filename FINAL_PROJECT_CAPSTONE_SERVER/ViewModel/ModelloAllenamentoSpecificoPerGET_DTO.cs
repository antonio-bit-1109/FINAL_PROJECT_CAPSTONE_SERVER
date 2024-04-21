namespace FINAL_PROJECT_CAPSTONE_SERVER.ViewModel
{
	public class ModelloAllenamentoSpecificoPerGET_DTO
	{
		public int IdAllenamento { get; set; }
		public string NomeAllenamento { get; set; }
		public double DurataTotaleAllenamento { get; set; }
		public int SerieTotali { get; set; }
		public int RipetizioniTotali { get; set; }

		public int DIfficoltaMedia { get; set; }

		public List<EsercizioDTO> Esercizi { get; set; }
	}
}
