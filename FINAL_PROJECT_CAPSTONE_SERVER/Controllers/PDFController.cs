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
				DateTime dataEOra = AllenamentoComplete.dataEOraComplete;
				string data = dataEOra.ToString("d");
				string ora = dataEOra.ToString("t");
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
					eserciziHtml.AppendLine($@"
					<li style='list-style: none; margin: 10px 0; line-height: 30px;'>
						<span style='display: inline-block; width: 20%; font-weight: 600; font-size: 18px;'>{esercizio.Esercizio.NomeEsercizio}</span>
						<span style='display: inline-block; width: 20%; font-size: 16px;'>{esercizio.Esercizio.ParteDelCorpo}</span>
						<span style='display: inline-block; width: 20%; font-size: 20px;'>{esercizio.Esercizio.Serie}</span>
						<span style='display: inline-block; width: 20%;'>X</span>
						<span style='display: inline-block; width: 20%; font-size: 20px;'>{esercizio.Esercizio.Ripetizioni}</span>
					</li>");

				}

				string htmlContent = $@"
							<div style='margin: 20px auto; max-width: 600px; padding: 20px; border: 1px solid #ccc; background-color: #FFFFFF; font-family: Arial, sans-serif; text-align: center;'>
								<div style='position: relative;'>   
									<img src='https://localhost:7098/logo/logoBello.png' alt='Logo' style='max-width: 100px; margin-bottom: 10px; position:absolute; top:0; right:0;'>
									<p style='margin-block: 4px'>Completato il: <br> </p>
								</div>
								<div>
									<span style='font-size: 20px'>{data} </span>
									-
									<span style='font-size: 20px'>{ora} </span>
								</div>
								<div>
									<h2 style='font-size: 24px; font-weight: bold;'>{NomeAllenamento.ToUpper()}</h2>
									<h4> <span style='margin-right: 4px;'>Difficoltà:</span>{difficolta}</h4>
								</div>
								<div style='margin-bottom: 20px; color: #333;'>
									<p style='margin: 0;'><span style='font-size: 22px;'>Durata Totale:</span> <span style='font-size: 20px; font-style: italic; margin-left: 10px;'>{DurataTotAllenamento} <span style=' margin-start: 5px;'>minuti</span> </p>
									<p style='margin: 0;'><span style='font-size: 22px;'>Serie Totali:</span> <span style='font-size: 20px; font-style: italic; margin-left: 10px;'>{TotaleSerie}</span></p>
									<p style='margin: 0;'><span style='font-size: 22px;'>Ripetizioni Totali:</span> <span style='font-size: 20px; font-style: italic; margin-left: 10px;'>{TotaleReps}</span></p>
								</div>
								<div style='color: #333;'>
									<ul style='padding: 0;'>
										{eserciziHtml}
									</ul>
								</div>
							</div>";




				PdfGenerator.AddPdfPages(pdfDocument, htmlContent, PageSize.A3);
				byte[]? response = null;
				using (MemoryStream ms = new MemoryStream())
				{
					pdfDocument.Save(ms);
					response = ms.ToArray();
				}
				string fileName = $"AllenamentoCompletato - {AllenamentoComplete.Allenamento.NomeAllenamento}" + ".pdf";
				return File(response, "application/pdf", fileName);
			}

			return BadRequest();
		}
	}
}
