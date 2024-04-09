using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{

	[Route("[controller]")]
	[ApiController]
	public class UtenteController : ControllerBase
	{

		private readonly ApplicationDbContext _db;

		public UtenteController(ApplicationDbContext db)
		{
			_db = db;
		}

		[HttpGet("getInfoUtente")]
		public async Task<IActionResult> GetUtenteLoggato()
		{

			var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Jti);

			if (userIdClaim != null)
			{

				int idUtente = Convert.ToInt32(userIdClaim.Value);

				var utente = await _db.Utenti.FindAsync(idUtente);
				return Ok(utente);
			}

			return BadRequest(new { messaggio = "Utente non trovato" });
		}



		[HttpPost("cambiaImmagineProfilo/{idUtente}")]
		public async Task<IActionResult> CambiaImmagine([FromRoute] string idUtente, [FromForm] IFormFile immagineProfilo)
		{

			if (immagineProfilo == null || immagineProfilo.Length == 0)
			{
				return BadRequest("Nessuna immagine fornita");
			}

			if (ModelState.IsValid)
			{
				var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
				var fileName = Path.GetRandomFileName() + Path.GetExtension(immagineProfilo.FileName);
				var imagePath = Path.Combine(wwwrootPath, "img-utenti", fileName);

				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					await immagineProfilo.CopyToAsync(stream);
				}
				var Utente = await _db.Utenti.FindAsync(Convert.ToInt32(idUtente));

				if (Utente != null)
				{
					Utente.ImmagineProfilo = fileName;
					await _db.SaveChangesAsync();

					return Ok(new { message = "Immagine cambiata con successo" });
				}

				return BadRequest(new { messaggio = "Esercizio Appena Creato è null." });
			}

			return BadRequest(new { messaggio = "qualcosa è andato storto" });
		}
	}
}
