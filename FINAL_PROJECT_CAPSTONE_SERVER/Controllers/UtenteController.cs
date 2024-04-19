using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using FINAL_PROJECT_CAPSTONE_SERVER.Models;
using FINAL_PROJECT_CAPSTONE_SERVER.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{
	[Authorize]
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


		[HttpPost("CheckPassword")]
		public async Task<IActionResult> checkPasswordUtente([FromBody] string password)
		{
			var IdUtenteLoggato = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

			if (IdUtenteLoggato != null)
			{
				var UtenteLoggato = await _db.Utenti.Where(t => t.IdUtente == Convert.ToInt32(IdUtenteLoggato)).FirstOrDefaultAsync();

				if (UtenteLoggato != null)
				{
					if (UtenteLoggato.Password == password)
					{
						return Ok(new { message = "Password Corrispondenti." });

					}
					return BadRequest(new { message = "Password non Corrispondenti." });
				}
				return BadRequest(new { message = "utente non trovato." });
			}

			return BadRequest(new { message = "Errore. idUtente non trovato nel server " });
		}


		[HttpPost("modificaDatiUtente/{idUtente}")]
		public async Task<IActionResult> modificaDati([FromBody] ModificaDatiUtenteDTO datiUtente, [FromRoute] int idUtente)
		{
			if (ModelState.IsValid)
			{

				var IdUtenteLoggato = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

				if (Convert.ToInt32(IdUtenteLoggato) == idUtente)
				{
					var UtenteLoggato = await _db.Utenti.Where(t => t.IdUtente == idUtente).FirstOrDefaultAsync();

					if (UtenteLoggato != null)
					{
						if (datiUtente.Nome == "" && datiUtente.Cognome == "" && datiUtente.Email == "")
						{
							return BadRequest(new { message = "I campi da Modificare sono tutti vuoti." });
						}

						if (datiUtente.Nome != null)
						{
							if (datiUtente.Nome == "")
							{
								UtenteLoggato.Nome = UtenteLoggato.Nome;
							}
							else
							{
								UtenteLoggato.Nome = datiUtente.Nome;
							}
						}

						if (datiUtente.Cognome != null)
						{
							if (datiUtente.Cognome == "")
							{
								UtenteLoggato.Cognome = UtenteLoggato.Cognome;
							}
							else
							{
								UtenteLoggato.Cognome = datiUtente.Cognome;
							}


						}

						if (datiUtente.Email != null)
						{
							if (datiUtente.Email == "")
							{
								UtenteLoggato.Email = UtenteLoggato.Email;
							}
							else
							{
								UtenteLoggato.Email = datiUtente.Email;
							}


						}


						_db.Update(UtenteLoggato);
						await _db.SaveChangesAsync();
						return Ok(new { message = "Modifiche Effettuate", UtenteLoggato });
					}
				}
			}
			return BadRequest();
		}


		[HttpDelete("cancellaAccount/{idUtente}")]
		public async Task<IActionResult> deleteAccount(int idUtente)
		{
			var IdUtenteLoggato = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

			if (IdUtenteLoggato != null)
			{
				if (Convert.ToInt32(IdUtenteLoggato) == idUtente)
				{
					var UtenteLoggato = await _db.Utenti.Where(t => t.IdUtente == idUtente).FirstOrDefaultAsync();


					if (UtenteLoggato != null)
					{
						_db.Remove(UtenteLoggato);
						await _db.SaveChangesAsync();

						return Ok(new { message = "Account Cancellato con Successo." });
					}

				}
			}

			return BadRequest();
		}
	}
}
