using FINAL_PROJECT_CAPSTONE_SERVER.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class EmailController : ControllerBase
	{
		[HttpPost]
		[Route("sendConfermaAcquisto")]
		public async Task<IActionResult> SendEmail([FromBody] EmailAcquistoConfermatoDTO email)
		{
			if (ModelState.IsValid)
			{
				if (email != null)
				{

					var from = new MailAddress("antonio.rizzuti@outlook.com", "Admin-Applicazione-Workout");
					var to = new MailAddress(email.to);
					const string fromPassword = "Ar11091995!.!";
					string subject = email.subject;

					double totaleFinale = 0;

					StringBuilder prodottoSpec = new StringBuilder();
					foreach (var prodotto in email.listaAcquisti)
					{

						prodottoSpec.AppendLine($"_________________________________");
						prodottoSpec.AppendLine($"Hai acquistato: {prodotto.nomeProdotto}");
						prodottoSpec.AppendLine($"Descrizione: {prodotto.descrizione}");
						prodottoSpec.AppendLine($"Prezzo: {prodotto.prezzoProdotto}€");
						prodottoSpec.AppendLine($"Quantità: {prodotto.quantita}");
						prodottoSpec.AppendLine($"Prezzo Relativo Prodotti: {prodotto.quantita * prodotto.prezzoProdotto}€");
						prodottoSpec.AppendLine($"_________________________________");
						prodottoSpec.AppendLine();
						totaleFinale += prodotto.quantita * prodotto.prezzoProdotto;
					}

					string body = Convert.ToString(prodottoSpec);

					var smtp = new SmtpClient
					{
						Host = "smtp-mail.outlook.com",
						Port = 587,
						EnableSsl = true,
						DeliveryMethod = SmtpDeliveryMethod.Network,
						UseDefaultCredentials = false,
						Credentials = new NetworkCredential(from.Address, fromPassword)
					};

					using (var message = new MailMessage(from, to)
					{
						Subject = subject,
						Body = body + $" Prezzo Totale Acquisti: {totaleFinale} €",

					})
					{
						smtp.Send(message);
					}
					return Ok();

				}

			}

			return BadRequest();
		}


		[HttpPost]
		[Route("sendConfermaIscrizione")]
		public async Task<IActionResult> MailConfermaIscrizione([FromBody] UtenteDTO datiutente)
		{

			if (ModelState.IsValid)
			{
				if (datiutente != null)
				{
					var from = new MailAddress("antonio.rizzuti@outlook.com", "Admin-Applicazione-Workout");
					var to = new MailAddress(datiutente.email);
					const string fromPassword = "Ar11091995!.!";
					string subject = "Benvenuto nel tuo nuovo Portale Workout!";


					StringBuilder bodyRegistrazione = new StringBuilder();
					bodyRegistrazione.AppendLine($"Grazie Per la Tua Iscrizione, {datiutente.nome}! ");
					bodyRegistrazione.AppendLine($"Ecco un recap dei tuoi dati di Registrazione:");
					bodyRegistrazione.AppendLine($"_________________________________");
					bodyRegistrazione.AppendLine($"Nome: {datiutente.nome}");
					bodyRegistrazione.AppendLine($"Cognome: {datiutente.cognome}");
					bodyRegistrazione.AppendLine($"Email: {datiutente.email}");
					bodyRegistrazione.AppendLine($"Password: {datiutente.password}");
					bodyRegistrazione.AppendLine($"_________________________________");
					bodyRegistrazione.AppendLine($"Mantieni sempre la tua Password in un luogo sicuro.");


					string body = Convert.ToString(bodyRegistrazione);


					var smtp = new SmtpClient
					{
						Host = "smtp-mail.outlook.com",
						Port = 587,
						EnableSsl = true,
						DeliveryMethod = SmtpDeliveryMethod.Network,
						UseDefaultCredentials = false,
						Credentials = new NetworkCredential(from.Address, fromPassword)
					};

					using (var message = new MailMessage(from, to)
					{
						Subject = subject,
						Body = body,


					})
					{
						smtp.Send(message);
					}
					return Ok();
				}
			}

			return BadRequest();
		}
	}
}
