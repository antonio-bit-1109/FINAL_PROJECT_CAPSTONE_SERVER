namespace FINAL_PROJECT_CAPSTONE_SERVER.ViewModel
{
	public class DatiUtenteAs_AdminDTO
	{
		public string nomeUtente { get; set; }
		public string cognomeUtente { get; set; }
		public double peso { get; set; }
		public int altezza { get; set; }
		public string email { get; set; }
		public bool easterEggFounded { get; set; }

		public bool UtentePremium { get; set; }
		public string? dataInizioAbbonamento { get; set; }
		public string? dataFineAbbonamento { get; set; }
	}
}
