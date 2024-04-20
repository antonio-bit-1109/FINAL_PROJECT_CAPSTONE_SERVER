using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using FINAL_PROJECT_CAPSTONE_SERVER.Models;
using FINAL_PROJECT_CAPSTONE_SERVER.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{
	[AllowAnonymous]
	[Route("[controller]")]
	[ApiController]
	public class AuthController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly ApplicationDbContext _db;

		public AuthController(IConfiguration configuration, ApplicationDbContext db)
		{
			_configuration = configuration;
			_db = db;
		}



		[HttpPost("token")]
		public IActionResult CreateToken([FromBody] ViewModelLogin login)
		{
			var user = Authenticate(login);

			if (user != null)
			{
				var tokenString = BuildToken(user);

				//come salvo una variabile nella sessione ?? voglio salvare lo user in sessione


				return Ok(
					new
					{
						token = tokenString,
						Utente = new
						{
							user.IdUtente,
							user.Nome,
							user.Ruolo,
							user.Email,
							user.ImmagineProfilo,
							//user.IdTrainer
						}
					}
				);
			}

			return Unauthorized();
		}
		private Utente Authenticate(ViewModelLogin login)
		{
			var user = _db.Utenti.FirstOrDefault(u =>
				u.Nome == login.Username && u.Password == login.Password
			);

			if (user == null)
			{
				return null;
			}

			return new Utente
			{
				IdUtente = user.IdUtente,
				Nome = user.Nome,
				Ruolo = user.Ruolo,
				Email = user.Email,
				ImmagineProfilo = user.ImmagineProfilo,
				//IdTrainer = user.IdTrainer
			};
		}

		private string BuildToken(Utente user)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Jti, Convert.ToString( user.IdUtente)),
				new Claim(JwtRegisteredClaimNames.Name, user.Nome),
				new Claim(ClaimTypes.Role, user.Ruolo),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),

			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				_configuration["Jwt:Issuer"],
				_configuration["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddDays(7),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}



		[HttpPost("registrazione")]
		public ActionResult Registrazione([FromBody] UtenteDTO utente)
		{
			if (ModelState.IsValid)
			{
				var emailGiaPresente = _db.Utenti.FirstOrDefault(x => x.Email == utente.email);

				if (emailGiaPresente != null)
				{
					return BadRequest(new { message = " Email già presente. Cambia la tua mail." });
				}


				Utente NuovoUtente = new Utente
				{
					Nome = utente.nome,
					Cognome = utente.cognome,
					Password = utente.password,
					ImmagineProfilo = "default.jpg",
					Ruolo = "utente",
					Email = utente.email,
					Altezza = utente.Altezza,
					Peso = utente.Peso

				};

				_db.Utenti.Add(NuovoUtente);
				_db.SaveChanges();
				return Ok(NuovoUtente);
			}

			return BadRequest("Errore");
		}
	}
}
