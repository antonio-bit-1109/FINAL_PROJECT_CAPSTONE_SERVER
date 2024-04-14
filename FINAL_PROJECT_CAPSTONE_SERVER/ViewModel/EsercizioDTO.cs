namespace FINAL_PROJECT_CAPSTONE_SERVER.ViewModel
{
	public class EsercizioDTO
	{
		public string nomeEsercizio { get; set; }
		public string immagineEsercizio { get; set; }

		public int serie { get; set; }
		public int ripetizioni { get; set; }

		public int recupero { get; set; }

		public int difficolta { get; set; }

		public bool IsStrenght { get; set; }
	}
}
