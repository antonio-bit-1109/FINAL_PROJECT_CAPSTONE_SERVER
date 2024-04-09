using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using FINAL_PROJECT_CAPSTONE_SERVER.Models;
using FINAL_PROJECT_CAPSTONE_SERVER.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class EserciziController : ControllerBase
	{

		private readonly ApplicationDbContext _context;

		public EserciziController(ApplicationDbContext context)
		{
			_context = context;
		}



		[HttpGet("list/{parteCorpo?}/{difficolta?}/{tipologia}")]
		public async Task<ActionResult<IEnumerable<Esercizio>>> GetEserciziConParametro([FromRoute] string? parteCorpo, string? difficolta, bool isstrenght)
		{

			// tutti i posibili casi se parte corpo è null 
			if (parteCorpo == "null")
			{
				if (difficolta == "null")
				{
					return await _context.Esercizi.Where(e => e.IsStrenght == isstrenght).ToListAsync();

				}
				return await _context.Esercizi.Where(e => e.IsStrenght == isstrenght && e.Difficolta == Convert.ToInt32(difficolta)).ToListAsync();

			}

			// tutti i casi possibili se difficolta è null
			if (difficolta == "null")
			{
				if (parteCorpo == "null")
				{
					return await _context.Esercizi.Where(e => e.IsStrenght == isstrenght).ToListAsync();

				}
				return await _context.Esercizi.Where(e => e.IsStrenght == isstrenght && e.ParteDelCorpoAllenata == parteCorpo).ToListAsync();
			}

			// tutti i casi possibili se tipologia è false
			if (isstrenght == false)
			{
				if (parteCorpo == "null")
				{
					return await _context.Esercizi.Where(e => e.Difficolta == Convert.ToInt32(difficolta)).ToListAsync();
				}

				return await _context.Esercizi.Where(e => e.ParteDelCorpoAllenata == parteCorpo && e.Difficolta == Convert.ToInt32(difficolta)).ToListAsync();

			}

			// se tutti i parametri esistono 
			return await _context.Esercizi.Where(e => e.ParteDelCorpoAllenata == parteCorpo && e.Difficolta == Convert.ToInt32(difficolta) && e.IsStrenght == isstrenght).ToListAsync();

		}

		[HttpPost("CreateEsercizio")]
		public async Task<IActionResult> createEsercizio([FromBody] CreateEsercizioDTO esercizio)
		{

			if (ModelState.IsValid)
			{
				// crea un nuovo esercizio a partire dal DTO ricevuto

				Esercizio nuovoEsercizio = new Esercizio
				{
					NomeEsercizio = esercizio.NomeEsercizio,
					DescrizioneEsercizio = esercizio.DescrizioneEsercizio,
					Difficolta = esercizio.DifficoltaEsercizio,
					IsStrenght = esercizio.IsStrenght,
					TempoRecupero = esercizio.TempoRecupero,
					Serie = esercizio.SerieEsercizio,
					Ripetizioni = esercizio.RipetizioniEsercizio,
					ParteDelCorpoAllenata = esercizio.ParteCorpoEsercizio,
					ImmagineEsercizio = "gif-default.gif"
				};

				_context.Esercizi.Add(nuovoEsercizio);
				await _context.SaveChangesAsync();

				int IdEsercizioCreato = nuovoEsercizio.IdEsercizio;

				return Ok(new { idEsercizio = IdEsercizioCreato, esercizioCreato = nuovoEsercizio });
			}

			return BadRequest(new { messaggio = "qualcosa è andato storto" });
		}


		[HttpPost("AggiungiImmagine/{idEsercizio}")]
		public async Task<IActionResult> AggiungiImmagineEsercizio([FromRoute] int idEsercizio, [FromForm] IFormFile immagineEsercizio)
		{
			if (immagineEsercizio == null || immagineEsercizio.Length == 0)
			{
				return BadRequest("Nessuna immagine fornita");
			}

			if (ModelState.IsValid)
			{
				var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
				var fileName = Path.GetRandomFileName() + Path.GetExtension(immagineEsercizio.FileName);
				var imagePath = Path.Combine(wwwrootPath, "img-esercizi", fileName);

				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					await immagineEsercizio.CopyToAsync(stream);
				}

				var EsercizioappenaCreato = await _context.Esercizi.FindAsync(Convert.ToInt32(idEsercizio));

				if (EsercizioappenaCreato != null)
				{
					EsercizioappenaCreato.ImmagineEsercizio = fileName;
					await _context.SaveChangesAsync();

					return Ok(new { messaggio = "Immagine Aggiunta Correttamente" });
				}

				return BadRequest(new { messaggio = "Esercizio Appena Creato è null." });
			}

			return BadRequest(new { messaggio = "qualcosa è andato storto" });
		}
	}
}
