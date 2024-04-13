namespace FINAL_PROJECT_CAPSTONE_SERVER.ViewModel
{
	public class ProdottoAcquistatoDTO
	{
		public string nomeProdotto { get; set; }
		public double PrezzoProdotto { get; set; }

		public string ImmagineProdotto { get; set; }

		public int quantita { get; set; }

		public DateTime dataAcquisto { get; set; }

		public double PrezzoTotaleTransazione { get; set; }
	}
}
