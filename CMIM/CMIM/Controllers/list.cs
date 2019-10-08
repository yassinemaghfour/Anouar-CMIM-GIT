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
    [Route("api/list")]
    public class list : Controller
    {
        private readonly CmimDBContext context;

        public list(CmimDBContext contextd)
        {
            context = contextd;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Rembouslist> Get()
        {
            return context.list;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getlist([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listrem = await context.list.FindAsync(id);

            if (listrem == null)
            {
                return NotFound();
            }

            return Ok(listrem);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Postlist([FromBody] Rembouslist rembouslist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.list.Add(rembouslist);
            await context.SaveChangesAsync();

            var dos = await context.Dossiers.FindAsync(rembouslist.Dossierreferance);

            if (dos == null)
            {
                return NotFound();
            }
            else
            {
                dos.rembourse = rembouslist.montant;
                dos.etat = "Remboursé";
                dos.rembourssementId = rembouslist.RembouserembourssementId;
                dos.diff = rembouslist.montant - dos.avance;
                context.Entry(dos).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
          
            
            //rembouslist.Dossier.montant = rembouslist.montant;
              
         //   rembouslist.Dossier.Rembouse.rembourssementId = rembouslist.RembouserembourssementId;
            
          
           
           // await context.SaveChangesAsync();
            
            //var dossiers = .Where(d => d.referance == rembouslist.dossierID);
            //var lis = context.list.Where(l => l.rembourssementId == rembouse.rembourssementId);
            //foreach (Dossier d in dossiers)
            //{
            //  //  foreach (Rembouslist l in lis)
            //    //{
            //        if (d.referance == rembouslist.dossierID)
            //        {
            //            d.etat = "rembourser";
            //            d.rembourse = rembouslist.montant;
            //        d.rembourssementId = rembouslist.rembourssementId;
            //            context.Entry(d).State = EntityState.Modified;
            //        }
            //  //  }
            //}

            return CreatedAtAction("Getlist", new { id = rembouslist.listId }, rembouslist);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
