namespace FINAL_PROJECT_CAPSTONE_SERVER.ViewModel
{
	public class EmailAcquistoConfermatoDTO
	{
		//TO sara utente a cui arriverà email , subject è il titolo della mail, body è il corpo della mail
		// posso inserire come corpo della mail gli oggetti che sono stati acquistati ?? 

		public string to { get; set; }
		public string subject { get; set; } = "Grazie per il tuo acquisto!";
		public List<ProdottoDTO> listaAcquisti { get; set; }


	}
}
