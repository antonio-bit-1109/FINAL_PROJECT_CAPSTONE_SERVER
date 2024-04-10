using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using FINAL_PROJECT_CAPSTONE_SERVER.Models;
using FINAL_PROJECT_CAPSTONE_SERVER.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.IdentityModel.Tokens.Jwt;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AbbonamentiController : ControllerBase
	{
		private readonly ApplicationDbContext _db;

		public AbbonamentiController(ApplicationDbContext db)
		{
			_db = db;
		}

		//[HttpGet("getAbbonamenti")]
		//public async Task<IActionResult> GetAbbonamenti()
		//{
		//	var abbonamenti = await _db.Abbonamenti.ToListAsync();

		//	if (abbonamenti != null)
		//	{
		//		return Ok(abbonamenti);

		//	}
		//	return BadRequest(new { message = "nessun Abbonamento trovato" });
		//}

		//[HttpGet("SubscriptionList")]
		//public IActionResult Get()
		//{
		//	return Ok(_db.SubscriptionPlans);
		//}

		// Endpoint per creare un nuovo piano di abbonamento
		[HttpPost("CreazionePianoAbbonamento")]
		public IActionResult CreateSubscriptionPlan([FromBody] SubscriptionPlanDTO subscriptionPlan)
		{
			// Creazione del prodotto per il piano di abbonamento su Stripe
			var productService = new ProductService();
			var productOptions = new ProductCreateOptions
			{
				Name = subscriptionPlan.NomeAbbonamento,
			};
			var product = productService.Create(productOptions);

			// Creazione del prezzo per il piano di abbonamento su Stripe
			var priceService = new PriceService();
			var priceOptions = new PriceCreateOptions
			{
				Product = product.Id,
				UnitAmount = subscriptionPlan.Price * 100,
				Currency = "eur",
				Recurring = new PriceRecurringOptions
				{
					Interval = subscriptionPlan.Durata,
				},
			};
			var price = priceService.Create(priceOptions);

			// Salvataggio del nuovo piano di abbonamento nel database locale
			var newSubscriptionPlan = new Abbonamento
			{
				NomeAbbonamento = subscriptionPlan.NomeAbbonamento,
				PrezzoAbbonamento = subscriptionPlan.Price,
				DescrizioneAbbonamento = subscriptionPlan.Description,
				StripePriceId = price.Id,
			};

			_db.Abbonamenti.Add(newSubscriptionPlan);
			_db.SaveChanges();

			return Ok(newSubscriptionPlan);
		}


		[HttpPost("CreaSessionPagamento/{IdAbbonamento}")]
		public ActionResult CreateCheckoutSession([FromRoute] int IdAbbonamento)
		{
			var IdUtente = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

			var Abbonamento = _db.Abbonamenti.Find(IdAbbonamento);
			var Utente = _db.Utenti.Find(Convert.ToInt32(IdUtente));

			if (Abbonamento == null || Utente == null)
			{
				return NotFound();
			}

			// Se l'utente non ha un StripeCustomerId, creiamo un nuovo cliente Stripe
			if (string.IsNullOrEmpty(Utente.StripeCustomerId))
			{
				var customers = new CustomerService();
				var customerOptions = new CustomerCreateOptions
				{
					Email = Utente.Email,
					// Puoi aggiungere qui altre opzioni se necessario
				};
				var customer = customers.Create(customerOptions);

				// Salva l'ID del cliente Stripe nel tuo database
				Utente.StripeCustomerId = customer.Id;
				_db.Utenti.Update(Utente);
				_db.SaveChanges();
			}

			var options = new SessionCreateOptions
			{
				PaymentMethodTypes = new List<string>
				{
					"card",
				},
				LineItems = new List<SessionLineItemOptions>
				{
					new SessionLineItemOptions
					{
						Price = Abbonamento.StripePriceId,
						Quantity = 1,
					},
				},
				Mode = "subscription",
				SuccessUrl = "http://localhost:5173/?session_id={CHECKOUT_SESSION_ID}", // URL del tuo frontend React
				CancelUrl = "http://localhost:5173/cancel", // URL del tuo frontend React
				Customer = Utente.StripeCustomerId,
			};

			var service = new SessionService();
			Session session = service.Create(options);

			return Ok(new { id = session.Id });
		}

		//[HttpGet("AbbonamentiDisponibili")]
		//public IActionResult AbbonamentiDisponibili()
		//{

		//}
	}
}







