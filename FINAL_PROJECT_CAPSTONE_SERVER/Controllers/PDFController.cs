using FINAL_PROJECT_CAPSTONE_SERVER.ViewModel.allenamentoCompletatoDTO;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using System.Text;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PDFController : ControllerBase
	{
		[HttpPost("generatePDF")]
		public async Task<IActionResult> createPdf([FromBody] AllenamentoCompletatoDTO AllenamentoComplete)
		{

			if (ModelState.IsValid)
			{
				var pdfDocument = new PdfDocument();
				//DateTime formatoData = DateTime.Parse(AllenamentoComplete.dataEOraComplete);
				//string Data = dataEOraComplete.ToString("dd/MM/yyyy");
				//string ora = dataEOraComplete.ToString("HH:mm:ss");
				string difficolta;
				switch (AllenamentoComplete.Allenamento.DifficoltaMediaAllenamento)
				{
					case 1:
						difficolta = "FACILE";
						break;
					case 2:
						difficolta = "MEDIO";
						break;
					case 3:
						difficolta = "DIFFICILE";
						break;
					default:
						difficolta = "N/A";
						break;
				}

				double DurataTotAllenamento = AllenamentoComplete.Allenamento.DurataTotaleAllenamento;
				string NomeAllenamento = AllenamentoComplete.Allenamento.NomeAllenamento;
				int TotaleSerie = AllenamentoComplete.Allenamento.TotaleSerie;
				int TotaleReps = AllenamentoComplete.Allenamento.TotaleRipetizioni;

				StringBuilder eserciziHtml = new StringBuilder();
				foreach (var esercizio in AllenamentoComplete.Allenamento.EserciziInAllenamento)
				{
					eserciziHtml.AppendLine($"<li>{esercizio.Esercizio.NomeEsercizio} {esercizio.Esercizio.ParteDelCorpo}  {esercizio.Esercizio.Serie} X {esercizio.Esercizio.Ripetizioni}</li>");
				}

				string htmlContent = $@"
                                 <div style='padding: 4rem;'>
                                    <div>
                                        <p style='margin-block: 4px'>Completato il: <br> </p>
                                    </div>
                                    <div style='display: flex; align-items: center; gap: 5px'>
                                        <p>{AllenamentoComplete.dataEOraComplete}</p>
                                        -
                                        <p>{AllenamentoComplete.dataEOraComplete}</p>
                                        <div style='margin: auto'><p>{difficolta}</p></div>
                                    </div>
                                    <div>
                                        <h1 style='margin-block: 15px'>{NomeAllenamento}</h1>
                                    </div>
                                    <div>
                                        <h2 style='margin-block: 15px'>Durata Totale: {DurataTotAllenamento}</h2>
                                    </div>
                                    <div>
                                        <h2 style='margin-block: 15px'>Serie Totali: {TotaleSerie}</h2>
                                    </div>
                                    <div>
                                        <h2 style='margin-block: 15px'>Ripetizioni Totali: {TotaleReps}</h2>
                                    </div>

                                    <div>
                                        <ul>
											{eserciziHtml}
                                        </ul>
                                    </div>
                                </div>";



				PdfGenerator.AddPdfPages(pdfDocument, htmlContent, PageSize.A4);
				byte[]? response = null;
				using (MemoryStream ms = new MemoryStream())
				{
					pdfDocument.Save(ms);
					response = ms.ToArray();
				}
				string fileName = "FeesStructure" + "nomeFile" + ".pdf";
				return File(response, "application/pdf", fileName);
			}

			return BadRequest();
		}
	}
}
