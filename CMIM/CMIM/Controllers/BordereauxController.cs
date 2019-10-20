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
    public class BordereauxController : ControllerBase
    {
        private readonly CmimDBContext _context;
        private int m = 0;
        public BordereauxController(CmimDBContext context)
        {
            _context = context;
        }

        // GET: api/Bordereaux
        [HttpGet]
        public IEnumerable<Bordereau> GetBordereau()
        {
            return _context.Bordereau;
        }

        // GET: api/Bordereaux/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBordereau([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bordereau = _context.Bordereau.Where(B => B.BordereauId == id).Include(B => B.Dossiers).FirstOrDefault();

            if (bordereau == null)
            {
                return NotFound();
            }

            return Ok(bordereau);
        }

        // PUT: api/Bordereaux/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBordereau([FromRoute] long id, [FromBody] Bordereau bordereau)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bordereau.BordereauId)
            {
                return BadRequest();
            }

            _context.Entry(bordereau).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BordereauExists(id))
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

        // POST: api/Bordereaux
        [HttpPost]
        public async Task<IActionResult> PostBordereau([FromBody] Bordereau bordereau)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _context.Bordereau.Add(bordereau);
            await _context.SaveChangesAsync();
            
                var dossiers = _context.Dossiers
                    .Where(d => (d.employee.company.ToLower() == bordereau.Company.ToLower() && d.etat == "Ouvert"))
                    .Take(5)
                    .OrderBy(d => d.date);
            
                foreach(Dossier D in dossiers) {
                    D.BordereauId = bordereau.BordereauId;
                    D.etat = "Avancé";
                     _context.Entry(D).State = EntityState.Modified;
                }

              await _context.SaveChangesAsync();

            return CreatedAtAction("GetBordereau", new { id = bordereau.BordereauId }, bordereau);
        }

        // DELETE: api/Bordereaux/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBordereau([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bordereau = await _context.Bordereau.FindAsync(id);
            if (bordereau == null)
            {
                return NotFound();
            }

            _context.Bordereau.Remove(bordereau);
            await _context.SaveChangesAsync();

            return Ok(bordereau);
        }

        private bool BordereauExists(long id)
        {
            return _context.Bordereau.Any(e => e.BordereauId == id);
        }
    }
}