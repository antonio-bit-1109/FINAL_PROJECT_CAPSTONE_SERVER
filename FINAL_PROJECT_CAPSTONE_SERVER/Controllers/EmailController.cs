using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using FINAL_PROJECT_CAPSTONE_SERVER.ViewModel;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class EmailController : ControllerBase
	{

		private readonly ApplicationDbContext _context;

		public EmailController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		[Route("sendConfermaAcquisto")]
		public IActionResult SendEmailConfermaAcquisto([FromBody] EmailAcquistoConfermatoDTO email)
		{
			if (ModelState.IsValid)
			{
				if (email != null)
				{
					var message = new MimeMessage();
					message.From.Add(MailboxAddress.Parse("antoniorizzuti767@gmail.com"));
					message.To.Add(MailboxAddress.Parse(email.to));
					message.Subject = email.subject;

					double totaleFinale = 0;

					var bodyBuilder = new BodyBuilder(); // Use BodyBuilder to create the body of the email

					foreach (var prodotto in email.listaAcquisti)
					{
						bodyBuilder.HtmlBody += $"<hr>";
						bodyBuilder.HtmlBody += $"<p><strong>Hai acquistato:</strong> {prodotto.nomeProdotto}</p>";
						bodyBuilder.HtmlBody += $"<p><strong>Descrizione:</strong> {prodotto.descrizione}</p>";
						bodyBuilder.HtmlBody += $"<p><strong>Prezzo Unitario:</strong> {prodotto.prezzoProdotto}€</p>";
						bodyBuilder.HtmlBody += $"<p><strong>Quantità:</strong> {prodotto.quantita}</p>";
						bodyBuilder.HtmlBody += $"<p><strong>Prezzo Relativo Prodotti:</strong> {prodotto.quantita * prodotto.prezzoProdotto}€</p>";
						totaleFinale += prodotto.quantita * prodotto.prezzoProdotto;
					}

					bodyBuilder.HtmlBody += $"<p><strong> Prezzo Totale : </strong> {totaleFinale} €";

					message.Body = bodyBuilder.ToMessageBody();

					using var smtp = new SmtpClient();
					smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
					smtp.Authenticate("antoniorizzuti767@gmail.com", "hoyyhhftkzywdgsf");
					smtp.Send(message);
					smtp.Disconnect(true);

					return Ok();
				}
			}
			return BadRequest();
		}


		[HttpPost("ConfermaIscrizione")]
		public IActionResult SendConfermaIscrizione([FromBody] UtenteDTO datiutente)
		{
			if (ModelState.IsValid)
			{

				if (datiutente != null)
				{
					var message = new MimeMessage();
					message.From.Add(MailboxAddress.Parse("antoniorizzuti767@gmail.com"));
					message.To.Add(MailboxAddress.Parse(datiutente.email));
					message.Subject = "Grazie per tua Iscrizione al nostro Portale!";


					var bodyBuilder = new BodyBuilder(); // Use BodyBuilder to create the body of the email

					bodyBuilder.HtmlBody += $"<hr>";
					bodyBuilder.HtmlBody += $"<p>Grazie per la tua iscrizione {datiutente.nome}!</p>";
					bodyBuilder.HtmlBody += $"<p> 👇 Qui sotto trovi un recap dei tuoi dati  👇 </p>";
					bodyBuilder.HtmlBody += $"<p> Il tuo nome Utente: {datiutente.nome}</p>";
					bodyBuilder.HtmlBody += $"<p>Cognome: {datiutente.cognome}</p>";
					bodyBuilder.HtmlBody += $"<p> LA TUA PASSWORD ---> {datiutente.password} <--- </p>";
					bodyBuilder.HtmlBody += $"<p> La tua email :{datiutente.email}</p>";
					bodyBuilder.HtmlBody += $"<p>Mi raccomando, non condividere la tua password con nessuno zio! </p>";

					message.Body = bodyBuilder.ToMessageBody();
					using var smtp = new SmtpClient();
					smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
					smtp.Authenticate("antoniorizzuti767@gmail.com", "hoyyhhftkzywdgsf");
					smtp.Send(message);
					smtp.Disconnect(true);

					return Ok();
				}
			}
			return BadRequest();
		}




	}




	//[HttpPost]
	//[Route("ConfermaIscrizione")]
	//public IActionResult confermaIscrizionesito ([FromBody] UtenteDTO datiutente)
	//{

	//	if (ModelState.IsValid)
	//	{
	//		if (datiutente != null)
	//		{
	//			var from = new MailAddress("antonio.rizzuti@outlook.com", "Admin-Applicazione-Workout");
	//			var to = new MailAddress(datiutente.email);
	//			const string fromPassword = "Ar11091995!.!";
	//			string subject = "Benvenuto nel tuo nuovo Portale Workout!";


	//			StringBuilder bodyRegistrazione = new StringBuilder();
	//			bodyRegistrazione.AppendLine($"Grazie Per la Tua Iscrizione, {datiutente.nome}! ");
	//			bodyRegistrazione.AppendLine($"Ecco un recap dei tuoi dati di Registrazione:");
	//			bodyRegistrazione.AppendLine($"_________________________________");
	//			bodyRegistrazione.AppendLine($"Nome: {datiutente.nome}");
	//			bodyRegistrazione.AppendLine($"Cognome: {datiutente.cognome}");
	//			bodyRegistrazione.AppendLine($"Email: {datiutente.email}");
	//			bodyRegistrazione.AppendLine($"Password: {datiutente.password}");
	//			bodyRegistrazione.AppendLine($"_________________________________");
	//			bodyRegistrazione.AppendLine($"Mantieni sempre la tua Password in un luogo sicuro.");


	//			string body = Convert.ToString(bodyRegistrazione);


	//			var smtp = new SmtpClient
	//			{
	//				Host = "smtp-mail.outlook.com",
	//				Port = 587,
	//				EnableSsl = true,
	//				DeliveryMethod = SmtpDeliveryMethod.Network,
	//				UseDefaultCredentials = false,
	//				Credentials = new NetworkCredential(from.Address, fromPassword)
	//			};

	//			using (var message = new MailMessage(from, to)
	//			{
	//				Subject = subject,
	//				Body = body,


	//			})
	//			{
	//				smtp.Send(message);
	//			}
	//			return Ok();
	//		}
	//	}

	//	return BadRequest();
	//}




}

