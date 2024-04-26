using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using FINAL_PROJECT_CAPSTONE_SERVER.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class EasterEggController : ControllerBase
	{
		private readonly ApplicationDbContext _db;

		public EasterEggController(ApplicationDbContext db)
		{
			_db = db;
		}


		[HttpPost("CreazioneAbbonamentoEasterEgg")]
		public async Task<IActionResult> CreaAbbonamentoRegalo()
		{

			Abbonamento abbonamentoEasterEgg = new Abbonamento
			{
				NomeAbbonamento = "Abbonamento EasterEgg - Bonus",
				DescrizioneAbbonamento = "Abbonamento Regalo",
				PrezzoAbbonamento = 0,
				DataInizioAbbonamento = DateTime.Now,
				DataFineAbbonamento = DateTime.Now.AddDays(5),
				IsActive = true,
				DurataAbbonamento = 5
			};

			_db.Abbonamenti.Add(abbonamentoEasterEgg);
			await _db.SaveChangesAsync();
			return Ok(new { idAbbonamento = abbonamentoEasterEgg.IdAbbonamento });

		}


		[HttpPost("AggiornaAbbonamentoUtente/{idAbbonamento}")]
		public async Task<IActionResult> abbonamentoGratis([FromRoute] int idAbbonamento)
		{

			var IdutenteLoggato = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

			if (IdutenteLoggato == null)
			{
				return StatusCode(500, "idutente non disponibile.");
			}

			var UtenteLoggato = await _db.Utenti.FindAsync(Convert.ToInt32(IdutenteLoggato));

			if (UtenteLoggato == null)
			{
				return StatusCode(500, "Nessun utente corrispondente all id fornito.");
			}

			// se utente ha gia un abbonamento attivo aggiungo 5 giorni al suo attuale abbonamento e cancello quello cancellato in precedenza
			if (UtenteLoggato.DataInizioAbbonamento != null && UtenteLoggato.DataFineAbbonamento != null && UtenteLoggato.IdAbbonamento != null)
			{
				UtenteLoggato.DataFineAbbonamento = UtenteLoggato.DataFineAbbonamento.Value.AddDays(5);
				_db.Utenti.Update(UtenteLoggato);

				var Abbonamentocreatoprima = await _db.Abbonamenti.FindAsync(idAbbonamento);

				if (Abbonamentocreatoprima == null)
				{
					return StatusCode(500, "nessun abbonamento trovato.");
				}

				_db.Abbonamenti.Remove(Abbonamentocreatoprima);

				await _db.SaveChangesAsync();

				return Ok(new { message = "aggiunti 5 giorni all' abbonamento dell utente." });
			}

			// se utente non ha un abbonamento attivo prendo quello creato in preceenza e glielo attribuisco.
			if (UtenteLoggato.DataInizioAbbonamento == null && UtenteLoggato.DataFineAbbonamento == null && UtenteLoggato.IdAbbonamento == null)
			{
				UtenteLoggato.IdAbbonamento = idAbbonamento;
				UtenteLoggato.DataInizioAbbonamento = DateTime.Now;
				UtenteLoggato.DataFineAbbonamento = DateTime.Now.AddDays(5);
				UtenteLoggato.IsPremium = true;

				_db.Utenti.Update(UtenteLoggato);
				await _db.SaveChangesAsync();
			}

			return Ok(new { message = "abbonamento regalo inserito con successo." });
		}



	}


}





