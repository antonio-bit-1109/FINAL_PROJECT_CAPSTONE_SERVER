using MailKit.Net.Smtp;
using MimeKit;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Models
{
	public class EmailSender
	{
		public Task SendEmailAsync(string emailReceiver, string subject, string htmlMessage)
		{
			var emailTosend = new MimeMessage();
			emailTosend.From.Add(MailboxAddress.Parse("antoniorizzuti767@gmail.com"));
			emailTosend.To.Add(MailboxAddress.Parse(emailReceiver));
			emailTosend.Subject = subject;
			emailTosend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

			using (var emailClient = new SmtpClient())
			{
				emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
				emailClient.Authenticate("antoniorizzuti767@gmail.com", "hoyyhhftkzywdgsf");
				emailClient.Send(emailTosend);
				emailClient.Disconnect(true);
			}
			return Task.CompletedTask;
		}
	}
}
