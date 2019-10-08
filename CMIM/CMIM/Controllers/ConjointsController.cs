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
	public class ConjointsController : ControllerBase
	{
		private readonly CmimDBContext _context;

		public ConjointsController(CmimDBContext context)
		{
			_context = context;
			//if (_context.Conjoint.Count() == 0)
			//{
			//	_context.Conjoint.Add(new Conjoint { Firstname = "Touria", Lastname = "Saki", DateNs = DateTime.Now, Employeematricule = "EMCVBCHFG" });
			//	_context.Conjoint.Add(new Conjoint { Firstname = "njasjoaisdj", Lastname = "sdfs", DateNs = DateTime.Now, Employeematricule = "EMCVBCHFG" });
			//	_context.Conjoint.Add(new Conjoint { Firstname = "sdf", Lastname = "Sakifs", DateNs = DateTime.Now, Employeematricule = "EMCVBCHFG" });
			//	_context.Conjoint.Add(new Conjoint { Firstname = "fsdfs", Lastname = "Saki", DateNs = DateTime.Now, Employeematricule = "EMCVBCHFG" });
			//	_context.Conjoint.Add(new Conjoint { Firstname = "dfs", Lastname = "dsff", DateNs = DateTime.Now, Employeematricule = "EMCVBCHFG" });
			//	_context.Conjoint.Add(new Conjoint { Firstname = "Touria", Lastname = "Saki", DateNs = DateTime.Now, Employeematricule = "EMP312312" });
			//	_context.SaveChangesAsync();
			//}
			 
		}

		// GET: api/Conjoints
		[HttpGet]
		public IEnumerable<Conjoint> GetConjoint()
		{
			return _context.Conjoint;
		}
        [HttpGet("emp/{empmat}")]
        public IEnumerable<Conjoint> GetConjointByEmp(string empmat)
        {
            return _context.Conjoint
                .Where(con => con.Employeematricule == empmat);

        }
        // GET: api/Conjoints/5
        [HttpGet("{id}")]
		public async Task<IActionResult> GetConjoint([FromRoute] long id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var conjoint = await _context.Conjoint.FindAsync(id);

			if (conjoint == null)
			{
				return NotFound();
			}
			return Ok(conjoint);
		}
        // PUT: api/Conjoints/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConjoint([FromRoute] long id, [FromBody] Conjoint conjoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != conjoint.ConjointId)
            {
                return BadRequest();
            }

            _context.Entry(conjoint).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConjointExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // POST: api/Conjoints
        [HttpPost]
        public async Task<IActionResult> PostConjoint([FromBody] Conjoint conjoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Conjoint.Add(conjoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConjoint", new { id = conjoint.ConjointId }, conjoint);
        }
        // DELETE: api/Conjoints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConjoint([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conjoint = await _context.Conjoint.FindAsync(id);
            if (conjoint == null)
            {
                return NotFound();
            }

            _context.Conjoint.Remove(conjoint);
            await _context.SaveChangesAsync();

            return Ok(conjoint);
        }
        private bool ConjointExists(long id)
        {
            return _context.Conjoint.Any(e => e.ConjointId == id);
        }
        [HttpGet("emp/{emp}")]
        public async Task<List<Conjoint>> getConjointByEmp([FromRoute] string emp)
        {
            var a =  _context.Conjoint.Where(c => c.Employeematricule == emp);
            return await a.ToListAsync();
        }
    }
}