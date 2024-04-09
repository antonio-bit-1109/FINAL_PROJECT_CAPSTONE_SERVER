namespace FINAL_PROJECT_CAPSTONE_SERVER.ViewModel.allenamentoCompletatoDTO
{
	public class AllenamentoCompletatoDTO
	{
		public int IdAllenamentoCompletato { get; set; }
		public int IdUtente { get; set; }
		public DateTime dataEOraComplete { get; set; }

		public AllenamentoDTO2 Allenamento { get; set; }
	}
}
