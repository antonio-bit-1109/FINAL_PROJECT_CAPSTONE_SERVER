using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using FINAL_PROJECT_CAPSTONE_SERVER.Models;
using FINAL_PROJECT_CAPSTONE_SERVER.ViewModel;
using FINAL_PROJECT_CAPSTONE_SERVER.ViewModel.allenamentoFiltratoDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{
	[Authorize]
	[Route("[controller]")]
	[ApiController]
	public class AllenamentoController : ControllerBase
	{

		private readonly ApplicationDbContext _context;

		public AllenamentoController(ApplicationDbContext context)
		{
			_context = context;
		}


		//vorrei fare la get di tutti gli allenamenti presenti nel db, compresi di specifiche degli esercizi

		[HttpGet("ListaAllenamenti")]
		public async Task<ActionResult<IEnumerable<ModelloAllenamentoSpecificoPerGET_DTO>>> GetAllenamenti()
		{


			var allenamenti = await _context.Allenamenti
				.Include(t => t.EserciziInAllenamento)
				.ThenInclude(es => es.Esercizio)
				.Select(a => new ModelloAllenamentoSpecificoPerGET_DTO
				{
					IdAllenamento = a.IdAllenamento,
					NomeAllenamento = a.NomeAllenamento,
					DurataTotaleAllenamento = a.DurataTotaleAllenamento,
					SerieTotali = a.TotaleSerie,
					DIfficoltaMedia = a.EserciziInAllenamento.Any() ? a.EserciziInAllenamento.Sum(eia => eia.Esercizio.Difficolta) / a.EserciziInAllenamento.Count() : 0,
					RipetizioniTotali = a.TotaleRipetizioni,
					Esercizi = a.EserciziInAllenamento.Select(eia => new EsercizioDTO
					{

						nomeEsercizio = eia.Esercizio.NomeEsercizio,
						immagineEsercizio = eia.Esercizio.ImmagineEsercizio,
						serie = eia.Esercizio.Serie,
						recupero = eia.Esercizio.TempoRecupero,
						ripetizioni = eia.Esercizio.Ripetizioni,
						IsStrenght = eia.Esercizio.IsStrenght,
						difficolta = eia.Esercizio.Difficolta,
						met = eia.Esercizio.MET

					}).ToList()
				})
				  .ToListAsync();

			return Ok(allenamenti);
		}


		// ricerca allenamenti filtrati per nome e per difficoltà
		[HttpPost("AllenamentoFiltrato")]
		public async Task<IActionResult> allenamentoFiltrato([FromBody] ParametriNomeDifficoltaDTO datas)
		{

			if (ModelState.IsValid)
			{
				var allenamenti = await _context.Allenamenti

				.Where(t =>
					(datas.NomeAllenamentoInput == null || t.NomeAllenamento.Contains(datas.NomeAllenamentoInput))
					&&
					(datas.difficoltainput == null || (t.EserciziInAllenamento.Any() && t.EserciziInAllenamento.Sum(eia => eia.Esercizio.Difficolta) / t.EserciziInAllenamento.Count() == datas.difficoltainput)))
				.Include(t => t.EserciziInAllenamento)
				.ThenInclude(es => es.Esercizio)
				.Select(a => new ModelloAllenamentoSpecificoPerGET_DTO
				{
					IdAllenamento = a.IdAllenamento,
					NomeAllenamento = a.NomeAllenamento,
					DurataTotaleAllenamento = a.DurataTotaleAllenamento,
					SerieTotali = a.TotaleSerie,
					DIfficoltaMedia = a.EserciziInAllenamento.Any() ? a.EserciziInAllenamento.Sum(eia => eia.Esercizio.Difficolta) / a.EserciziInAllenamento.Count() : 0,
					RipetizioniTotali = a.TotaleRipetizioni,
					Esercizi = a.EserciziInAllenamento.Select(eia => new EsercizioDTO
					{

						nomeEsercizio = eia.Esercizio.NomeEsercizio,
						immagineEsercizio = eia.Esercizio.ImmagineEsercizio,
						serie = eia.Esercizio.Serie,
						recupero = eia.Esercizio.TempoRecupero,
						ripetizioni = eia.Esercizio.Ripetizioni,
						IsStrenght = eia.Esercizio.IsStrenght,
						difficolta = eia.Esercizio.Difficolta,
						met = eia.Esercizio.MET

					}).ToList()
				})
				  .ToListAsync();

				return Ok(allenamenti);
			}

			return BadRequest();

		}


		//questo metodo riceve in entrata i dati degli esercizi tramite post,
		//i dati in entrata sono una serie di oggetti contenenti informazioni sull esercizio + nome dell'esercizio stabilito da front end
		// quensti dati saranno poi salvati nel db come record di tipo allenamento

		// nello stesso metodo, siccome si tratta di una relazione molti a molti, per ogni id esercizio fornito,
		// vengono creati una serie di record di tipo EserciziInallenamento che identificano quali esercizi sono presenti in ogni allenamento

		[HttpPost("AggiungiAllenamento")]
		public IActionResult CreaAllenamento([FromBody] AllenamentoDto ModelloAllenamento)
		{
			if (ModelState.IsValid)
			{
				Allenamento allenamento = new Allenamento
				{
					NomeAllenamento = ModelloAllenamento.NomeAllenamento,
					DurataTotaleAllenamento = (((ModelloAllenamento.DurataTotaleAllenamento) / 60) + 5),
					TotaleRipetizioni = ModelloAllenamento.RipetizioniTotali,
					TotaleSerie = ModelloAllenamento.SerieTotali

				};

				_context.Allenamenti.Add(allenamento);
				_context.SaveChanges();

				// subito dopo aver salvato l'allenamento
				// ho a disposizione l'id dell'allenamento appena creato
				// e posso utilizzarlo per creare i record nella tabella EserciziInAllenamento

				foreach (int idEsercizio in ModelloAllenamento.IdEsercizi)
				{
					EserciziInAllenamento eserciziInAllenamento = new EserciziInAllenamento
					{
						IdEsercizio = idEsercizio,
						IdAllenamento = allenamento.IdAllenamento
					};


					_context.EserciziInAllenamenti.Add(eserciziInAllenamento);
					_context.SaveChanges();
				}

				// mi creo una lista contenenti gli id degli esercizi aggiunti all'allenamento
				List<int> ListaIdEsercizi = ModelloAllenamento.IdEsercizi;

				return Ok(new
				{
					NomeAllenamentoCreatoUtente = allenamento.NomeAllenamento,
					ListaIdEserciziAssociati = ListaIdEsercizi,
					allenamento.IdAllenamento
				});

			}

			return StatusCode(400, new { Message = "Errore nella creazione dell'allenamento" });
		}


		// quando da frontend l'utente clicca sul bottone concludi allenamento, l'id dell allenamento creato, inviato tramite post , l'idutente,
		// presente nel claim del cookie di autenticazione  
		// e la data del momento della creazione vengono salvati in un nuovo record di tipo allenamentiCompletati,
		// che salva un record sul fatto che l'utente abbia concluso quell'allenamento
		[HttpPost("AllenamentoCompletato")]
		public async Task<IActionResult> PostAllenamentoCompletato([FromBody] IdAllenamentoArrivatoDaAllenamentoNellaLIstaConclusoDTO request)
		{
			if (ModelState.IsValid)
			{
				var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Jti);

				if (userIdClaim != null)
				{
					AllenamentoCompletato allenamentoCompletato = new AllenamentoCompletato
					{
						IdUtente = Convert.ToInt32(userIdClaim.Value),
						IdAllenamento = request.IdAllenamento,
						DataEOraDelCompletamento = DateTime.Now

					};

					_context.AllenamentiCompletati.Add(allenamentoCompletato);
					await _context.SaveChangesAsync();


					return Ok(allenamentoCompletato);//prendo idallenamento , prendo idutente dai claims e creo nuovo record in tabella allenamenticompletato
				}

				return StatusCode(400, new { Message = "Errore interno nel reperimento dell'utente." });

			}

			return StatusCode(400, new { Message = "Errore nell'inserimento dell' allenamento come allenamento completato." });

		}

		[HttpPost("aggiornaKcalConsumateUtente")]
		public async Task<IActionResult> aggiornaKcal([FromBody] double kcalBruciate)
		{

			if (ModelState.IsValid)
			{
				var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Jti);

				if (userIdClaim == null)
				{
					return BadRequest();
				}

				var utente = await _context.Utenti.Where(t => t.IdUtente == Convert.ToInt32(userIdClaim.Value)).FirstOrDefaultAsync();

				if (utente == null)
				{
					return BadRequest();
				}

				utente.TotaleKcalConsumate += Convert.ToDouble(kcalBruciate);
				_context.Update(utente);
				await _context.SaveChangesAsync();
				return Ok();
			}

			return BadRequest();

		}

		[HttpDelete("CancellaAllenamento/{id}")]
		public async Task<IActionResult> CancellaAllenamento(int id)
		{
			var allenamento = await _context.Allenamenti.FindAsync(id);

			if (allenamento == null)
			{
				return NotFound();
			}

			_context.Allenamenti.Remove(allenamento);
			await _context.SaveChangesAsync();

			return Ok(allenamento);
		}
	}
}
