namespace FINAL_PROJECT_CAPSTONE_SERVER.ViewModel.allenamentoCompletatoDTO
{
	public class AllenamentoDTO2
	{
		public int IdAllenamento { get; set; }
		public string NomeAllenamento { get; set; }
		public double DurataTotaleAllenamento { get; set; }

		public int TotaleSerie { get; set; }

		public int TotaleRipetizioni { get; set; }

		public int DifficoltaMediaAllenamento { get; set; }

		public List<EserciziInAllenamentoDTO2> EserciziInAllenamento { get; set; }
	}
}
