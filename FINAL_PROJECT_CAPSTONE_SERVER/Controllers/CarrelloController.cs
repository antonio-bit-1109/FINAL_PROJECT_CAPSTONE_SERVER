using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using FINAL_PROJECT_CAPSTONE_SERVER.Models;
using FINAL_PROJECT_CAPSTONE_SERVER.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{
	[Authorize]
	[Route("[controller]")]
	[ApiController]
	public class CarrelloController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IConfiguration _configuration;

		public CarrelloController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
			_configuration = configuration; // Assegna un valore a _configuration


		}


		[HttpPost("create-session")]
		public ActionResult CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest request)
		{
			//arrivo DTO carrello ottimizzato 

			var domain = "http://localhost:5173/"; // URL del tuo frontend React

			// Crea la lista degli articoli per la sessione di checkout
			var lineItems = request
				.ListaItems.Select(item => new SessionLineItemOptions
				{

					PriceData = new SessionLineItemPriceDataOptions
					{
						UnitAmountDecimal = (long)(item.prezzoProdotto * 100), // Converti il prezzo in centesimi
						Currency = "eur",
						ProductData = new SessionLineItemPriceDataProductDataOptions
						{
							Name = item.nomeProdotto,
							Description = item.descrizione,
						},
					},
					Quantity = item.quantita, // Modifica la quantità se necessario

				})
				.ToList();

			//come ricavo email dal token 
			var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

			if (userEmail == null)
			{
				userEmail = "default@default.com";
			}

			var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

			List<int> IdprodottiVenduti = new List<int>();

			foreach (var item in request.ListaItems)
			{
				ProdottoVeduto prodottoVenduto = new ProdottoVeduto
				{
					IdProdotto = item.idProdotto,
					IdUtente = Convert.ToInt32(userIdClaim),
					Quantita = item.quantita,
					Data = DateTime.Now,
					PrezzoTotTransazione = item.prezzoProdotto * item.quantita,
					IsPagato = false

				};

				_context.ProdottiVenduti.Add(prodottoVenduto);
				_context.SaveChanges();
				IdprodottiVenduti.Add(prodottoVenduto.IdProdottoVeduto);
			}

			string IdprodottiVendutiString = string.Join(",", IdprodottiVenduti);
			// Crea opzioni per la sessione di checkout
			var options = new SessionCreateOptions
			{
				Metadata = new Dictionary<string, string>
				{
					{ "idProdottiVenduti" ,IdprodottiVendutiString }
				},
				PaymentMethodTypes = new List<string> { "card" },
				CustomerEmail = userEmail,
				LineItems = lineItems,
				Mode = "payment",
				SuccessUrl = domain + "pagamentoSuccess",
				CancelUrl = domain + "pagamentoFailed",
			};

			var service = new SessionService();
			Session session = service.Create(options);

			return Ok(new { sessionId = session.Id });
			// Ora hai i claims dell'utente e puoi usarli per le tue operazioni
		}


		[HttpGet("storicoAcquisti/{idUtente}")]
		public async Task<IActionResult> GetStoricoAcquisti(string idUtente)
		{

			var ProdottiAcquistati = await _context.ProdottiVenduti.Where(t => t.IdUtente == Convert.ToInt32(idUtente))
				.Include(t => t.Prodotto).Select(a => new ProdottoAcquistatoDTO
				{
					nomeProdotto = a.Prodotto.NomeProdotto,
					PrezzoProdotto = a.Prodotto.PrezzoProdotto,
					ImmagineProdotto = a.Prodotto.ImmagineProdotto,
					quantita = a.Quantita,
					dataAcquisto = a.Data,
					PrezzoTotaleTransazione = a.PrezzoTotTransazione
				}).ToListAsync();

			var idUtenteLoggato = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

			if (idUtenteLoggato != null)
			{
				if (Convert.ToInt32(idUtenteLoggato) == Convert.ToInt32(idUtente))
				{
					if (idUtente != null)
					{
						return Ok(new { data = ProdottiAcquistati });
					}
				}
			}



			return BadRequest();
		}
	}
}



