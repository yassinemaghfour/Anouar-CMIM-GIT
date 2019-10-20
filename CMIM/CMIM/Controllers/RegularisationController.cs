using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CMIM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Text;

namespace CMIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegularisationController : ControllerBase
    {
        private readonly CmimDBContext _context;

        public RegularisationController(CmimDBContext context)
        {
            _context = context;
        }

        // GET: api/Régularisation
        [HttpGet]
        public IEnumerable<Regularisation> GetRegularisation()
        {
            return _context.Regularisation;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegularisation([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var régularisation = _context.Regularisation.Include(R => R.listDesRégularisation)
                .ThenInclude(R => R.Employee)
                .Where(R => R.RegularisationID == id).FirstOrDefault();

            if (régularisation == null)
            {
                return NotFound();
            }

            return Ok(régularisation);
        }


        // POST: api/Bordereaux
        [HttpPost]
        public async Task<IActionResult> PostRegularisation([FromBody] Regularisation régularisation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Regularisation.Add(régularisation);
            await _context.SaveChangesAsync();

            var Dossiers_Emp = _context.Dossiers
              .Where(d => d.date.Month == régularisation.dateRegularisation.Month
                      && d.date.Year == régularisation.dateRegularisation.Year && d.etat == "Remboursé")
                           .Select(X => new RegularisationVue
                           {
                               EmpMatricule = X.employeematricule,
                               MatriculeCmim = X.employee.matriculecmim,
                               NumDo = X.referance,
                               numBu = X.employee.buId,
                               Nom = X.employee.first_name + " " + X.employee.last_name,
                               Avance = X.avance,
                               Remboursee = X.rembourse,
                               Regul = soustraction(X.rembourse, X.avance),
                               RegularisationId = régularisation.RegularisationID
                           });

            var lst = Dossiers_Emp.ToList();

            double total = 0;

            foreach(var gr in Dossiers_Emp.GroupBy(d => new { d.EmpMatricule }).Select(X => new { EmpMatricule = X.Key.EmpMatricule, Total = X.Sum(i => soustraction(i.Remboursee , i.Avance)) })) {
                Console.WriteLine(gr.EmpMatricule+ ":" + gr.Total);
                Regul_Emp r = new Regul_Emp();
                
                r.EmployeeMatricule = gr.EmpMatricule;
                r.Total = gr.Total;
                r.Régularisation = régularisation;
                

                total += r.Total;
                _context.Regul_Emp.Add(r);
            }

            Regularisation reg = _context.Regularisation.FindAsync(régularisation.RegularisationID).Result;
            reg.Total = total;
            
            await _context.SaveChangesAsync();

            var Dossiers = _context.Dossiers.Where(d => d.date.Month == régularisation.dateRegularisation.Month
                        && d.date.Year == régularisation.dateRegularisation.Year && d.etat == "Remboursé");

            foreach(var doss in Dossiers) {
                doss.etat = "Régularisé";
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegularisation", lst);
        }

        public double soustraction(double rembouse, double avance)
        {
            return rembouse - avance;
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

        private bool RegularisationExists(long id)
        {
            return _context.Regularisation.Any(e => e.RegularisationID == id);
        }
    }
}
class RegularisationVue
{
    public string EmpMatricule;
    public string MatriculeCmim;
    public string NumDo;
    public string numBu;
    public string Nom;
    public double Avance;
    public double Remboursee;
    public double Regul;
    public long RegularisationId;
}