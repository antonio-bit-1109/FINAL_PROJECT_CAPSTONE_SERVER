using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using FINAL_PROJECT_CAPSTONE_SERVER.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddSession();

// Add IHttpContextAccessor service
// questo servizio serve per poter accedere all'HttpContext,
// utile ad esempio se vuoi richiamare i dati del cookie di autenticazione in un controller 
builder.Services.AddHttpContextAccessor();

// utilizzato per configurare
// la cache distribuita in memoria nel tuo progetto ASP.NET Core.
// Necessaria se vuoi usare le session in un progetto del genere a quanto pare. 
//builder.Services.AddDistributedMemoryCache();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
	options.AddPolicy(
		"AllowSpecificOrigin",
		builder =>
		{
			builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
		}
	);
});

builder
	.Services.AddAuthentication(opt =>
	{
		opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])

			)
		};
	});

// ultima cosa aggiunta 
builder.Services.AddAuthorization();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddTransient<EmailSender>();

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["SecretKey"];

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // Aggiungi questa linea

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

//app.UseSession();
app.MapControllers();

app.Run();
