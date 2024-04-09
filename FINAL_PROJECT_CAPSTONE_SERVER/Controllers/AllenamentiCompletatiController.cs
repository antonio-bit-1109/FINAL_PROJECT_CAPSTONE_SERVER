using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using FINAL_PROJECT_CAPSTONE_SERVER.ViewModel.allenamentoCompletatoDTO;
using FINAL_PROJECT_CAPSTONE_SERVER.ViewModel.esercizioPreferitoDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AllenamentiCompletatiController : ControllerBase
	{

		private readonly ApplicationDbContext _db;

		public AllenamentiCompletatiController(IConfiguration configuration, ApplicationDbContext db)
		{
			_db = db;
		}

		[HttpGet("CompletedWorkoutsUtente/{IdUtente}")]
		public async Task<IActionResult> AllenamentiCompletati(string IdUtente)
		{

			//1.Recupero dei dati: La prima parte del codice recupera i dati dal database.
			//	Utilizza il metodo ToListAsync per eseguire la query e restituire i risultati 
			//	come una lista di oggetti AllenamentoCompletato.
			var allenamentiCompletati = await _db.AllenamentiCompletati
		.Where(t => t.IdUtente == Convert.ToInt32(IdUtente))
		.Include(ac => ac.Allenamento)
		.ThenInclude(a => a.EserciziInAllenamento)
		.ThenInclude(eia => eia.Esercizio)
		.ToListAsync();


			//2.Trasformazione in DTO: La seconda parte del codice trasforma la lista di oggetti AllenamentoCompletato in una lista di DTO.
			//	Questo viene fatto utilizzando il metodo Select per creare un nuovo AllenamentoCompletatoDto per ogni AllenamentoCompletato nella lista.

			var allenamentoCompletatoDTO = allenamentiCompletati.Select(ac => new AllenamentoCompletatoDTO
			{
				IdAllenamentoCompletato = ac.IdAllenamentoCompletato,
				IdUtente = ac.IdUtente,
				dataEOraComplete = ac.DataEOraDelCompletamento,
				Allenamento = new AllenamentoDTO2
				{
					IdAllenamento = ac.Allenamento.IdAllenamento,
					NomeAllenamento = ac.Allenamento.NomeAllenamento,
					DurataTotaleAllenamento = ac.Allenamento.DurataTotaleAllenamento,
					TotaleRipetizioni = ac.Allenamento.TotaleRipetizioni,
					DifficoltaMediaAllenamento = ac.Allenamento.EserciziInAllenamento.Select(eia => eia.Esercizio.Difficolta).Sum() / ac.Allenamento.EserciziInAllenamento.Count(),
					TotaleSerie = ac.Allenamento.TotaleSerie,
					EserciziInAllenamento = ac.Allenamento.EserciziInAllenamento.Select(eia => new EserciziInAllenamentoDTO2
					{
						Esercizio = new EsercizioDTO2
						{
							NomeEsercizio = eia.Esercizio.NomeEsercizio,
							DescrizioneEsercizio = eia.Esercizio.DescrizioneEsercizio,
							Difficolta = eia.Esercizio.Difficolta,
							IsStrenght = eia.Esercizio.IsStrenght,
							Temporecupero = eia.Esercizio.TempoRecupero,
							Serie = eia.Esercizio.Serie,
							Ripetizioni = eia.Esercizio.Ripetizioni,
							ParteDelCorpo = eia.Esercizio.ParteDelCorpoAllenata
						}
					}).ToList()
				}


			}).ToList();


			return Ok(allenamentoCompletatoDTO);
		}



		[HttpGet("DettagliAllenamentiCompletati/{idUtente}")]
		public async Task<IActionResult> EsercizioPreferitoUtente(string IdUtente)
		{
			if (IdUtente != null)
			{
				try
				{
					var EsercizioPreferito = await _db.AllenamentiCompletati.Where(t => t.IdUtente == Convert.ToInt32(IdUtente))
					.Include(d => d.Allenamento)
					.ThenInclude(e => e.EserciziInAllenamento)
					.ThenInclude(s => s.Esercizio)
					.ToListAsync();

					var esercizioPreferito = EsercizioPreferito.SelectMany(ac => ac.Allenamento.EserciziInAllenamento)
					.GroupBy(eia => eia.Esercizio.NomeEsercizio)
					.OrderByDescending(group => group.Count())
					.FirstOrDefault();

					var difficoltamedia = EsercizioPreferito.SelectMany(ac => ac.Allenamento.EserciziInAllenamento)
						.GroupBy(eia => eia.Esercizio.Difficolta)
						.OrderByDescending(group => group.Count())
						.FirstOrDefault();

					var TipologiaEsercizioPreferita = EsercizioPreferito.SelectMany(ac => ac.Allenamento.EserciziInAllenamento)
						.GroupBy(eia => eia.Esercizio.IsStrenght)
						.OrderByDescending(group => group.Count())
						.FirstOrDefault()?.Key ?? false;



					if (esercizioPreferito == null || difficoltamedia == null)
					{
						return Ok(new { message = "Nessun esercizio preferito trovato." });
					}



					var esercizioPreferitoDTO = new EsercizioPreferitoDTO
					{
						NomeEsercizioPreferito = esercizioPreferito.Key
					};

					var difficoltaMediaDTO = new DifficoltaMediaDTO
					{
						DifficoltaMedia = difficoltamedia.Key
					};

					var tipoEsPrefe = new TipoEsercizioPreferitoDTO
					{
						TipologiaEsercizioPreferita = TipologiaEsercizioPreferita
					};

					return Ok(new { esercizioPreferitoDTO, difficoltaMediaDTO, tipoEsPrefe });



				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}

			return BadRequest("IdUtente null");
		}
	}
}
