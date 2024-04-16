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


		// in questo endpoint creo un nuovo abbonamento partendo dai dati forniti dal frontend e ritorno l'id dell abbonamentno creato
		[HttpPost("CreazionePianoAbbonamento")]
		public async Task<IActionResult> CreateSubscriptionPlan([FromBody] SubscriptionPlanDTO subscriptionPlan)
		{

			if (ModelState.IsValid)
			{
				// al momento della creazione di un piano d'abbonamento, se l'utente è gia premium,
				// quindi sottoscritto ad un abbonamento, non può crearne un altro 
				var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
				var UtenteLoggato = _db.Utenti.FirstOrDefault(u => u.IdUtente == Convert.ToInt32(userIdClaim));

				if (UtenteLoggato != null)
				{
					if (UtenteLoggato.IdAbbonamento != null && UtenteLoggato.IsPremium == true)
					{
						return BadRequest(new { message = "Hai già un abbonamento attivo, disdici quello Attualmente in uso prima di proseguire." });
					}
				}



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




		// passo id dell'abbonamento creato alla sessione di pagamento di stripe, da qui ricavo l'abbonamento dal db, 
		//ricavo idutente dal claim e utente loggato dal db e inserisco i dati dell abbonamento da acquistare e dell utente nel lineitemoption di stripe
		// i dati quali id abbonamento , idutente , e durata li salvo come metadata della sessione di strpe li riprenderò poi nel webhook

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

		[HttpPost("AnnullaAbbonamento")]
		public async Task<IActionResult> DisdiciAbbonamento()
		{

			var IdUtenteClaim = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

			var utente = _db.Utenti.FirstOrDefault(t => t.IdUtente == Convert.ToInt32(IdUtenteClaim));

			if (utente != null)
			{

				var AbbonamentoSottoscritto = _db.Abbonamenti.Where(t => t.IdAbbonamento == utente.IdAbbonamento).FirstOrDefault();

				if (utente.IdAbbonamento == null && utente.IsPremium == false && AbbonamentoSottoscritto == null)
				{
					return Ok(new { message = "Non sono presenti Abbonamenti attivi." });
				}

				if (AbbonamentoSottoscritto != null)
				{

					utente.IdAbbonamento = null;
					utente.IsPremium = false;
					utente.DataInizioAbbonamento = null;
					utente.DataFineAbbonamento = null;
					_db.Utenti.Update(utente);
					await _db.SaveChangesAsync();

					AbbonamentoSottoscritto.IsActive = false;
					AbbonamentoSottoscritto.DurataAbbonamento = 0;
					_db.Abbonamenti.Update(AbbonamentoSottoscritto);
					await _db.SaveChangesAsync();

					return Ok(new { message = "Abbonamento Annullato con Successo." });
				}

				return BadRequest();
			}


			return BadRequest();
		}



		// se il pagamento va a buon fine nel webhook imposto , sull utente che ha effettuato l'acquisto ilabbonamento acquistato e data inizio e fine abbonamento
		// sull abbonamento imposto invece che è attivo e data inizio e fine abbonamento
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

						if (metadata != null)
						{
							//string userId = metadata["userId"];
							if (metadata.TryGetValue("userId", out string userId))
							{
								if (metadata.TryGetValue("abbonamentoId", out string abbonamentoId))
								{
									if (metadata.TryGetValue("durataAbbonamento", out string durataAbbonamento))
									{
										var utenteCheEffettuaAcquisto = _db.Utenti.FirstOrDefault(t => t.IdUtente == Convert.ToInt32(userId));



										if (utenteCheEffettuaAcquisto != null && utenteCheEffettuaAcquisto.IsPremium == false)
										{
											utenteCheEffettuaAcquisto.IsPremium = true;
											utenteCheEffettuaAcquisto.IdAbbonamento = Convert.ToInt32(abbonamentoId);
											utenteCheEffettuaAcquisto.DataInizioAbbonamento = DateTime.Now;
											utenteCheEffettuaAcquisto.DataFineAbbonamento = DateTime.Now.AddDays(Convert.ToInt32(durataAbbonamento));

											_db.Utenti.Update(utenteCheEffettuaAcquisto);
											_db.SaveChanges();
										}


										var AbbonamentoAcquistato = _db.Abbonamenti.FirstOrDefault(t => t.IdAbbonamento == Convert.ToInt32(abbonamentoId));

										if (AbbonamentoAcquistato != null)
										{
											AbbonamentoAcquistato.IsActive = true;
											AbbonamentoAcquistato.DataInizioAbbonamento = DateTime.Now;
											AbbonamentoAcquistato.DataFineAbbonamento = DateTime.Now.AddDays(Convert.ToInt32(durataAbbonamento));
											_db.Abbonamenti.Update(AbbonamentoAcquistato);
											_db.SaveChanges();
										}

										return Ok(new { message = "Abbonamento acquistato e sottoscritto dall'utente" });


									}
								}




							}
							else if (metadata.TryGetValue("idProdottiVenduti", out string idProdottiVendutiString))
							{
								// Converti la stringa in una lista di interi
								List<int> idProdottiVenduti = idProdottiVendutiString.Split(',').Select(int.Parse).ToList();

								foreach (var item in idProdottiVenduti)
								{
									var ConfermaAcquistoprodotto = _db.ProdottiVenduti.FirstOrDefault(t => t.IdProdottoVeduto == item);

									if (ConfermaAcquistoprodotto != null)
									{
										ConfermaAcquistoprodotto.IsPagato = true;
										_db.ProdottiVenduti.Update(ConfermaAcquistoprodotto);
										_db.SaveChanges();
									}

								}

								return Ok(new { message = "Prodotto acquistato correttamente dall'utente!" });

							}

							return BadRequest();


						}

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







