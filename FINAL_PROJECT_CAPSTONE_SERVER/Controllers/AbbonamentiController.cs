using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

		[HttpGet("getAbbonamenti")]
		public async Task<IActionResult> GetAbbonamenti()
		{
			var abbonamenti = await _db.Abbonamenti.ToListAsync();

			if (abbonamenti != null)
			{
				return Ok(abbonamenti);

			}
			return BadRequest(new { message = "nessun Abbonamento trovato" });
		}
	}
}







//[HttpGet("SubscriptionList")]
//public IActionResult Get()
//{
//	return Ok(_db.SubscriptionPlans);
//}

//// Endpoint per creare un nuovo piano di abbonamento
//[HttpPost]
//public IActionResult CreateSubscriptionPlan([FromBody] SubscriptionPlanDTO subscriptionPlan)
//{
//	// Creazione del prodotto per il piano di abbonamento su Stripe
//	var productService = new ProductService();
//	var productOptions = new ProductCreateOptions
//	{
//		Name = subscriptionPlan.Description,
//	};
//	var product = productService.Create(productOptions);

//	// Creazione del prezzo per il piano di abbonamento su Stripe
//	var priceService = new PriceService();
//	var priceOptions = new PriceCreateOptions
//	{
//		Product = product.Id,
//		UnitAmount = subscriptionPlan.Price,
//		Currency = "eur",
//		Recurring = new PriceRecurringOptions
//		{
//			Interval = "month",
//		},
//	};
//	var price = priceService.Create(priceOptions);

//	// Salvataggio del nuovo piano di abbonamento nel database locale
//	var newSubscriptionPlan = new SubscriptionPlan
//	{
//		Price = subscriptionPlan.Price,
//		Description = subscriptionPlan.Description,
//		StripePriceId = price.Id,
//	};
//	_db.SubscriptionPlans.Add(newSubscriptionPlan);
//	_db.SaveChanges();

//	return Ok(newSubscriptionPlan);
//}

//// Endpoint per creare una sessione di pagamento per l'abbonamento
//[HttpPost("CreateBillingSession")]
//public ActionResult CreateCheckoutSession(int subscriptionPlanId)
//{
//	var subscriptionPlan = _db.SubscriptionPlans.Find(subscriptionPlanId);
//	if (subscriptionPlan == null)
//	{
//		return NotFound();
//	}

//	// Creazione di un nuovo cliente Stripe
//	var customerService = new CustomerService();
//	var customer = customerService.Create(new CustomerCreateOptions());

//	// Definizione delle opzioni per la creazione della sottoscrizione
//	var options = new SubscriptionCreateOptions
//	{
//		Customer = customer.Id,
//		Items = new List<SubscriptionItemOptions>
//		{
//			new SubscriptionItemOptions
//			{
//				Price = subscriptionPlan.StripePriceId,
//			},
//		},
//		Metadata = new Dictionary<string, string>
//		{
//			{ "description", subscriptionPlan.Description },
//		},
//	};

//	// Creazione della sottoscrizione su Stripe
//	var service = new SubscriptionService();
//	Stripe.Subscription subscription = service.Create(options);

//	return Ok(subscription);
//}