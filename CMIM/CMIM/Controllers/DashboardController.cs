using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMIM.Models;
using System.IO;

namespace Cmim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
		private readonly CmimDBContext _context;
		public DashboardController(CmimDBContext context)
		{
			_context = context;
		}
		[HttpGet]
		public async Task<ActionResult> GetDossiers()
		{
            var q = await _context.Dossiers
                .Where(o => o.etat == "Ouvert")
                .GroupBy(d => new { d.employee.company })
                .Select(s => new
                {
                    s.Key.company,
                    count = s.Count()
                })
                .ToListAsync()
				;
			Console.WriteLine(q);
			
			return Ok(q);
		}
		[HttpGet("{borderauID}")]
		public async Task<List<Dossier>> GetDossierByCompany([FromRoute] string borderauID)
		{
			var dossiers = await _context.Dossiers
				.Where(d => (d.BordereauId.ToString().Equals(borderauID)))
				.Take(25)
                .Include("employee")
				.OrderByDescending(d =>  d.date ).ToListAsync();
			return dossiers;
		}
        [HttpGet("lastDateByEmp/{matricule}")]
        public async Task<Dossier> getLastDateByEmp(string matricule)
        {
            return _context.Dossiers.Where(d => d.employeematricule == matricule)
                .OrderByDescending(d => d.dateLunette)
                .FirstOrDefault();
        }


	}
}