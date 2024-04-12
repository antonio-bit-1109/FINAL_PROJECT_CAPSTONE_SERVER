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

				return BadRequest(new { messaggio = "Esercizio Appena Creato è null." });
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


		//[HttpPost("CreaProdottoAcquistato")]
		//public IActionResult ProdottoAcquistatoSuccess([FromBody] CarrelloOttimizzatoDTO carrello)
		//{
		//	var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Jti);
		//	var usernameClaim = User.FindFirst(JwtRegisteredClaimNames.Name);
		//	var roleClaim = User.FindFirst(ClaimTypes.Role);

		//	if (userIdClaim != null && usernameClaim != null && roleClaim != null)
		//	{
		//		var userIdFromClaimToken = userIdClaim.Value;
		//		//var usernameFromClaimToken = usernameClaim.Value;
		//		//var roleFromClaimToken = roleClaim.Value;


		//		if (carrello != null && carrello.Prodotti != null)
		//		{

		//			if (ModelState.IsValid)
		//			{
		//				foreach (var prodotto in carrello.Prodotti)
		//				{

		//					ProdottoVeduto prodottoVenduto = new ProdottoVeduto()
		//					{
		//						IdProdotto = prodotto.IdProdotto,
		//						IdUtente = Convert.ToInt32(userIdFromClaimToken),
		//						Quantita = prodotto.quantita,
		//						Data = DateTime.Now,
		//						PrezzoTotTransazione = prodotto.prezzoProdotto * prodotto.quantita
		//					};

		//					_context.ProdottiVenduti.Add(prodottoVenduto);
		//					_context.SaveChanges();

		//				}
		//				return Ok();
		//			}
		//			return BadRequest("modello non valido.");

		//		}

		//		return BadRequest("carrello vuoto");

		//	}

		//	return Unauthorized();

		//}
		//// GET: Prodotto/5
		//[HttpGet("{id}")]
		//public async Task<ActionResult<Prodotto>> GetProdotto(int id)
		//{
		//	var prodotto = await _context.Prodotti.FindAsync(id);

		//	if (prodotto == null)
		//	{
		//		return NotFound();
		//	}

		//	return prodotto;
		//}

		//// PUT: Prodotto/5
		//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//[HttpPut("{id}")]
		//public async Task<IActionResult> PutProdotto(int id, Prodotto prodotto)
		//{
		//	if (id != prodotto.IdProdotto)
		//	{
		//		return BadRequest();
		//	}

		//	_context.Entry(prodotto).State = EntityState.Modified;

		//	try
		//	{
		//		await _context.SaveChangesAsync();
		//	}
		//	catch (DbUpdateConcurrencyException)
		//	{
		//		if (!ProdottoExists(id))
		//		{
		//			return NotFound();
		//		}
		//		else
		//		{
		//			throw;
		//		}
		//	}

		//	return NoContent();
		//}

		//// POST: Prodotto
		//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//[HttpPost]
		//public async Task<ActionResult<Prodotto>> PostProdotto(Prodotto prodotto)
		//{
		//	_context.Prodotti.Add(prodotto);
		//	await _context.SaveChangesAsync();

		//	return CreatedAtAction("GetProdotto", new { id = prodotto.IdProdotto }, prodotto);
		//}

		//// DELETE: Prodotto/5
		//[HttpDelete("{id}")]
		//public async Task<IActionResult> DeleteProdotto(int id)
		//{
		//	var prodotto = await _context.Prodotti.FindAsync(id);
		//	if (prodotto == null)
		//	{
		//		return NotFound();
		//	}

		//	_context.Prodotti.Remove(prodotto);
		//	await _context.SaveChangesAsync();

		//	return NoContent();
		//}

		//private bool ProdottoExists(int id)
		//{
		//	return _context.Prodotti.Any(e => e.IdProdotto == id);
		//}
	}
}

