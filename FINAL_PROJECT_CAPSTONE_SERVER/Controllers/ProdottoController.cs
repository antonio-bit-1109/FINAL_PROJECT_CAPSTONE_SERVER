using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using FINAL_PROJECT_CAPSTONE_SERVER.Models;
using FINAL_PROJECT_CAPSTONE_SERVER.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{
	[Authorize]
	[Route("[controller]")]
	[ApiController]
	public class ProdottoController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public ProdottoController(ApplicationDbContext context)
		{
			_context = context;
		}


		// GET: Prodotto
		[HttpGet("list")]
		public async Task<ActionResult<IEnumerable<Prodotto>>> GetProdotti()
		{
			return await _context.Prodotti.ToListAsync();
		}

		[HttpPost("CreateProdotto")]
		public async Task<IActionResult> CreaProdotto([FromBody] CreaProdottoDTO prodottoDaCreare)
		{
			if (ModelState.IsValid)
			{

				if (prodottoDaCreare != null)
				{
					Prodotto newProdotto = new Prodotto()
					{
						NomeProdotto = prodottoDaCreare.NomeProdotto,
						PrezzoProdotto = prodottoDaCreare.PrezzoProdotto,
						Descrizione = prodottoDaCreare.Descrizione,
						//ImmagineProdotto = immagineProdotto.FileName
					};

					_context.Prodotti.Add(newProdotto);
					await _context.SaveChangesAsync();

					int prodottoId = newProdotto.IdProdotto;

					return Ok(prodottoId);
				}
			}

			return BadRequest("modello non valido.");
		}


		[HttpPost("AggiungiImmagine/{idprodotto}")]
		public async Task<IActionResult> AggiungiImmagineProdotto([FromRoute] int idprodotto, [FromForm] IFormFile immagineProdotto)
		{
			if (immagineProdotto == null || immagineProdotto.Length == 0)
			{
				return BadRequest("Nessuna immagine fornita");
			}

			if (ModelState.IsValid)
			{
				var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
				var fileName = Path.GetRandomFileName() + Path.GetExtension(immagineProdotto.FileName);
				var imagePath = Path.Combine(wwwrootPath, "img-prodotti", fileName);

				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					await immagineProdotto.CopyToAsync(stream);
				}

				var ProdottoAppenaCreato = await _context.Prodotti.FindAsync(Convert.ToInt32(idprodotto));

				if (ProdottoAppenaCreato != null)
				{
					ProdottoAppenaCreato.ImmagineProdotto = fileName;
					await _context.SaveChangesAsync();

					return Ok(ProdottoAppenaCreato);
				}

				return BadRequest(new { messaggio = "Prodotto Appena Creato è null." });
			}

			return BadRequest(new { messaggio = "qualcosa è andato storto" });
		}

		[HttpDelete("cancellaProdotto/{idProdotto}")]
		public async Task<IActionResult> CancellaProdotto([FromRoute] string idProdotto)
		{
			if (idProdotto != null)
			{
				var trovaProdotto = await _context.Prodotti.FindAsync(Convert.ToInt32(idProdotto));

				if (trovaProdotto != null)
				{
					_context.Prodotti.Remove(trovaProdotto);
					await _context.SaveChangesAsync();

				}

				return Ok();
			}

			return BadRequest("idProdotto non arrivato.");
		}

		[HttpPost("ModificaProdotto/{idProdotto}")]
		public async Task<IActionResult> editProdotto([FromBody] ModificaProdottoDTO datiProdottoModifica, [FromRoute] int idProdotto)
		{
			if (datiProdottoModifica == null)
			{
				return BadRequest();
			}

			if (ModelState.IsValid)
			{

				var ProdottoDaModificare = await _context.Prodotti.FindAsync(idProdotto);

				if (ProdottoDaModificare == null)
				{
					return BadRequest();
				}

				ProdottoDaModificare.NomeProdotto = datiProdottoModifica.ProdottoNome;
				ProdottoDaModificare.PrezzoProdotto = Convert.ToDouble(datiProdottoModifica.prodottoPrezzo);
				ProdottoDaModificare.Descrizione = datiProdottoModifica.ProdottoDescrizione;

				_context.Prodotti.Update(ProdottoDaModificare);
				await _context.SaveChangesAsync();

				return Ok(new { message = "Prodotto Modificato con successo." });
			}

			return BadRequest();
		}

		[HttpPost("ModificaImmagine/{idProdotto}")]
		public async Task<IActionResult> ModificaImmagineProdotto([FromForm] IFormFile? immagineProdottoModifica, [FromRoute] int idProdotto)
		{
			if (immagineProdottoModifica == null)
			{
				return Ok(new { message = "nessun immagine fornita." });
			}


			if (ModelState.IsValid)
			{
				var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
				var fileName = Path.GetRandomFileName() + Path.GetExtension(immagineProdottoModifica.FileName);
				var imagePath = Path.Combine(wwwrootPath, "img-prodotti", fileName);

				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					await immagineProdottoModifica.CopyToAsync(stream);
				}

				var ProdottoDaMOdificare = await _context.Prodotti.FindAsync(idProdotto);

				if (ProdottoDaMOdificare != null)
				{
					ProdottoDaMOdificare.ImmagineProdotto = fileName;
					await _context.SaveChangesAsync();

					return Ok();
				}

				return BadRequest(new { messaggio = "Prodotto Da modificare è null." });
			}
			return BadRequest(new { messaggio = "Modello non valido." });

		}

	}

}


