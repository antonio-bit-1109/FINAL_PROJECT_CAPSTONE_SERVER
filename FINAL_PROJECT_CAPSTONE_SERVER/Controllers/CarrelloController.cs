using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using FINAL_PROJECT_CAPSTONE_SERVER.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{

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

		[Authorize]
		[HttpPost("create-session")]
		public ActionResult CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest request)
		{

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


			// Crea opzioni per la sessione di checkout
			var options = new SessionCreateOptions
			{
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



	}
}



