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
		private readonly IConfiguration _configuration;

		public AbbonamentiController(ApplicationDbContext db, IConfiguration configuration)
		{
			_db = db;
			_configuration = configuration;
		}


		//Endpoint per creare un nuovo piano di abbonamento
		[HttpPost("CreazionePianoAbbonamento")]
		public async Task<IActionResult> CreateSubscriptionPlan([FromBody] SubscriptionPlanDTO subscriptionPlan)
		{

			if (ModelState.IsValid)
			{
				Abbonamento nuovoAbbonamento = new Abbonamento
				{
					NomeAbbonamento = subscriptionPlan.NomeAbbonamento,
					PrezzoAbbonamento = subscriptionPlan.Price,
					DescrizioneAbbonamento = subscriptionPlan.Description,
					DurataAbbonamento = subscriptionPlan.DurataAbbonamento,
				};

				_db.Abbonamenti.Add(nuovoAbbonamento);
				await _db.SaveChangesAsync();

				int IdAbbonamentoCreato = nuovoAbbonamento.IdAbbonamento;
				return Ok(new { idAbbonamento = IdAbbonamentoCreato });
			}

			return BadRequest();
		}






		[HttpPost("CreaSessionPagamento/{IdAbbonamento}")]
		public ActionResult CreateCheckoutSession([FromRoute] int IdAbbonamento)
		{

			var domain = "http://localhost:5173/"; // URL del tuo frontend React

			// Recupera il prodotto dal database utilizzando l'ID del prodotto
			var abbonamento = _db.Abbonamenti.FirstOrDefault(p => p.IdAbbonamento == IdAbbonamento);


			if (abbonamento == null)
			{
				return NotFound();
			}

			var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
			var UtenteLoggato = _db.Utenti.FirstOrDefault(u => u.IdUtente == Convert.ToInt32(userIdClaim));

			if (UtenteLoggato == null)
			{
				return NotFound();
			}

			// Crea la lista degli articoli per la sessione di checkout
			var lineItems = new List<SessionLineItemOptions>
			{
				new SessionLineItemOptions
				{
					PriceData = new SessionLineItemPriceDataOptions
					{
						UnitAmountDecimal = (long)(abbonamento.PrezzoAbbonamento * 100), // Converti il prezzo in centesimi
                        Currency = "eur",
						ProductData = new SessionLineItemPriceDataProductDataOptions
						{
							Name = abbonamento.NomeAbbonamento,
							Description = abbonamento.DescrizioneAbbonamento,
						},
					},
					Quantity = 1, // Quantità di default è 1
                }
			};

			// Come ricavo email dal token
			var userEmail = UtenteLoggato.Email;

			if (userEmail == null)
			{
				userEmail = "default@default.com";
			}

			// Crea opzioni per la sessione di checkout
			var options = new SessionCreateOptions
			{
				Metadata = new Dictionary<string, string>
				{
					{ "userId", userIdClaim },
					{ "abbonamentoId", abbonamento.IdAbbonamento.ToString()},
					{ "durataAbbonamento" , abbonamento.DurataAbbonamento.ToString() }
				},
				PaymentMethodTypes = new List<string> { "card" },
				CustomerEmail = userEmail,
				LineItems = lineItems,
				Mode = "payment",
				SuccessUrl = domain + "?session_id={CHECKOUT_SESSION_ID}",
				CancelUrl = domain + "pagamentoFailed",
			};

			var service = new SessionService();
			Session session = service.Create(options);

			return Ok(new { sessionId = session.Id });
			// Ora hai i claims dell'utente e puoi usarli per le tue operazioni
		}


		[HttpPost("webhook")]
		public async Task<IActionResult> StripeWebhook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			var stripeSignatureHeader = Request.Headers["Stripe-Signature"];
			var secret = _configuration["Stripe:WebhookSecret"];

			try
			{
				var stripeEvent = EventUtility.ConstructEvent(json, stripeSignatureHeader, secret);

				// Gestisci l'evento
				if (stripeEvent.Type == Events.CheckoutSessionCompleted)
				{
					var session = stripeEvent.Data.Object as Session;


					if (session != null)
					{
						var sessionId = session.Id;

						var metadata = session.Metadata;

						var userId = metadata["userId"];
						var abbonamentoId = metadata["abbonamentoId"];
						var durataAbbonamento = metadata["durataAbbonamento"];

						var utenteCheEffettuaAcquisto = _db.Utenti.FirstOrDefault(t => t.IdUtente == Convert.ToInt32(userId));

						if (utenteCheEffettuaAcquisto != null)
						{
							utenteCheEffettuaAcquisto.IdAbbonamento = Convert.ToInt32(abbonamentoId);
							utenteCheEffettuaAcquisto.DataInizioAbbonamento = DateTime.Now;
							utenteCheEffettuaAcquisto.DataFineAbbonamento = DateTime.Now.AddDays(Convert.ToInt32(durataAbbonamento));

							_db.Utenti.Update(utenteCheEffettuaAcquisto);
							_db.SaveChanges();
						};

						var AbbonamentoAcquistato = _db.Abbonamenti.FirstOrDefault(t => t.IdAbbonamento == Convert.ToInt32(abbonamentoId));

						if (AbbonamentoAcquistato != null)
						{
							AbbonamentoAcquistato.IsActive = true;
							AbbonamentoAcquistato.DataInizioAbbonamento = DateTime.Now;
							AbbonamentoAcquistato.DataFineAbbonamento = DateTime.Now.AddDays(Convert.ToInt32(durataAbbonamento));
							_db.Abbonamenti.Update(AbbonamentoAcquistato);
							_db.SaveChanges();
						}

						return Ok(new { message = "Tutto Apposto!!" });
					}

				}

				return new EmptyResult();
			}
			catch (StripeException ex)
			{
				return BadRequest(new { message = ex });
			}
		}

	}
}







