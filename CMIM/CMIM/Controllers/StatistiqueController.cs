using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMIM.Models;

namespace Cmim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatistiqueController : ControllerBase
    {
		private readonly CmimDBContext _context;
		public StatistiqueController(CmimDBContext context)
		{
			_context = context;
		}
		[HttpGet]
		public async Task<IActionResult> GetDossiers()
		{
			var q = await  _context.Dossiers
				.GroupBy(d => new { d.etat })
				.Select(s => new {
					s.Key.etat,
					count = s.Count()
				}).ToListAsync()
				;
			Console.WriteLine(q);
			
			return Ok(q);
		}

	}
}