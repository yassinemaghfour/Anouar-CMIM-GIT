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
    public class DossiersController : ControllerBase
    {
        private readonly CmimDBContext _context;
        private double montantTotal;
        public DossiersController(CmimDBContext context)
        {
            _context = context;
            //var emp = _context.employees.Include(b => b.dossies).First();
            //var dos = new Dossier
            //{
            //	referance = "RF123456",
            //	date = DateTime.Now,
            //	montant = 10000,
            //	avance = 1069,
            //	rembourse = 1000,
            //	diff = 69,
            //	etat = "ouvert"
            //};
            //_context.SaveChangesAsync();

        }
        // GET: api/Dossiers
        [HttpGet]
        public IEnumerable<Dossier> GetDossiers()
        {
            return _context.Dossiers.Include(D => D.employee);
        }
        // GET: api/Dossiers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDossier([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dossier = await _context.Dossiers.FindAsync(id);

            if (dossier == null)
            {
                return NotFound();
            }

            return Ok(dossier);
        }

        // PUT: api/Dossiers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDossier([FromRoute] string id, [FromBody] Dossier dossier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dossier.referance)
            {
                return BadRequest();
            }
            montantTotal = dossier.MtGi_L + dossier.MtCadre_L + dossier.MtVisite_L + dossier.MtProthese_D + dossier.MtSoins_D + dossier.Devis_D + dossier.MtVisite_M 
                            + dossier.MtPharmacie_M + dossier.Mt_analyse + dossier.Mt_radio ;
            if (dossier.avance < montantTotal * 0.8)
            {
                dossier.avance = dossier.avance;
            }else dossier.avance = montantTotal * 0.8;

            dossier.diff = montantTotal - dossier.avance;
            dossier.etat = "Ouvert";    
            _context.Entry(dossier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DossierExists(id))
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

        // POST: api/Dossiers
        [HttpPost]
        public async Task<IActionResult> PostDossier([FromBody] Dossier dossier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            montantTotal = dossier.MtGi_L + dossier.MtCadre_L + dossier.MtVisite_L + dossier.MtProthese_D + dossier.MtSoins_D + dossier.Devis_D + dossier.MtVisite_M
                         + dossier.MtPharmacie_M + dossier.Mt_analyse + dossier.Mt_radio;

            dossier.avance = montantTotal * 0.8;
            dossier.diff = montantTotal - dossier.avance;
            dossier.etat = "Ouvert";
            _context.Dossiers.Add(dossier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDossier", new { id = dossier.referance }, dossier);

        }

        // DELETE: api/Dossiers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDossier([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dossier = await _context.Dossiers.FindAsync(id);
            if (dossier == null)
            {
                return NotFound();
            }

            _context.Dossiers.Remove(dossier);
            await _context.SaveChangesAsync();

            return Ok(dossier);
        }

        private bool DossierExists(string id)
        {
            return _context.Dossiers.Any(e => e.referance == id);
        }

        [HttpGet("emp/{emp}")]
        public async  Task<List<Dossier>> getDossiesByEmp([FromRoute] string emp)
        {
            var q = _context.Dossiers.Where(d => d.employeematricule == emp);
            return await q.ToListAsync();
        }
    }
}