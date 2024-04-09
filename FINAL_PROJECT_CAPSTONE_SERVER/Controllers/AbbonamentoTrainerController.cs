using FINAL_PROJECT_CAPSTONE_SERVER.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FINAL_PROJECT_CAPSTONE_SERVER.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AbbonamentoTrainerController : ControllerBase
	{
		private readonly ApplicationDbContext _db;

		public AbbonamentoTrainerController(ApplicationDbContext db)
		{
			_db = db;
		}

		[HttpGet("getTrainers")]
		public async Task<IActionResult> GetTrainers()
		{
			var trainers = await _db.Trainers.ToListAsync();

			if (trainers != null)
			{
				return Ok(trainers);

			}
			return BadRequest();
		}
	}
}
