using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMIM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cmim.Controllers
{
    [Route("api/NonPayee")]
    public class NonPayee : Controller
    {
        private readonly CmimDBContext _context;
        public NonPayee(CmimDBContext context)
        {
            this._context = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public ActionResult GetDossiers()
        {
            var q = _context.Dossiers
                .Include(d => d.employee)
                .Where(o => o.etat == "Avancé");
            Console.WriteLine(q);
            return Ok(q);
        }

        // GET api/<controller>/5
        [HttpGet("{etat}")]
        public List<Dossier> GetDossierNonPayee([FromRoute] string etat)
        {
            var dossiers = _context.Dossiers.Include(d => d.employee).Include(c => c.employee.conjoints).Include(e => e.employee.enfants    ).Where(d => (d.etat ==etat ));
            var dos = dossiers.ToList();
            for(int i=0;i<dos.Count;i++)
            {
                dos[i].etat = "Envoyé à la CMIM";
                _context.Entry(dos[i]).State = EntityState.Modified;
            }
            _context.SaveChangesAsync();
            return  dos;
		}
 
    }
}
