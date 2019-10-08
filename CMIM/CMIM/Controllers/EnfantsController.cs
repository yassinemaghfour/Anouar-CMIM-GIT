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
    public class EnfantsController : ControllerBase
    {
        private readonly CmimDBContext _context;

        public EnfantsController(CmimDBContext context)
        {
            _context = context;
            Console.WriteLine("ffffffffffffffffffffffff");
            //if (_context.Enfant.Count() == 0)
            //{
            //    _context.Enfant.Add(new Enfant { employeematricule = "EMCVBCHFG", DateNs = DateTime.Now, DateVs = DateTime.Now, Firstname = "Oussama", Lastname = "barrady", ConjointId = 37 });
            //    _context.Enfant.Add(new Enfant { employeematricule = "EMCVBCHFG", DateNs = DateTime.Now, DateVs = DateTime.Now, Firstname = "cccew", Lastname = "ecwce", ConjointId = 37 });
            //    _context.Enfant.Add(new Enfant { employeematricule = "EMCVBCHFG", DateNs = DateTime.Now, DateVs = DateTime.Now, Firstname = "hgfhhf", Lastname = "cwecwef", ConjointId = 37 });
            //    _context.Enfant.Add(new Enfant { employeematricule = "EMCVBCHFG", DateNs = DateTime.Now, DateVs = DateTime.Now, Firstname = "vbnbvn", Lastname = "tertert", ConjointId = 37 });
            //    _context.SaveChangesAsync();
            //}



        }

        // GET: api/Enfants
        [HttpGet]
        public IEnumerable<Enfant> GetEnfant()
        {
            return _context.Enfant;
        }

        // GET: api/Enfants/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnfant([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enfant = await _context.Enfant.FindAsync(id);

            if (enfant == null)
            {
                return NotFound();
            }

            return Ok(enfant);
        }

        // PUT: api/Enfants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnfant([FromRoute] long id, [FromBody] Enfant enfant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enfant.EnfantId)
            {
                return BadRequest();
            }

            _context.Entry(enfant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnfantExists(id))
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

        // POST: api/Enfants
        [HttpPost]
        public async Task<IActionResult> PostEnfant([FromBody] Enfant enfant)
        {
            Console.WriteLine(enfant.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Enfant.Add(enfant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnfant", new { id = enfant.EnfantId }, enfant);
        }

        // DELETE: api/Enfants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnfant([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enfant = await _context.Enfant.FindAsync(id);
            if (enfant == null)
            {
                return NotFound();
            }

            _context.Enfant.Remove(enfant);
            await _context.SaveChangesAsync();

            return Ok(enfant);
        }

        private bool EnfantExists(long id)
        {
            return _context.Enfant.Any(e => e.EnfantId == id);
        }

        [HttpGet("emp/{emp}")]
        public async  Task<List<Enfant>> getEnfantByEmp([FromRoute] string emp)
        {
            var q = _context.Enfant.Where(e => e.employeematricule == emp);
            return await q.ToListAsync();
        }
    }
}