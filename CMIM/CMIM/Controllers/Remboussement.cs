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
    [Route("api/Remboussement")]
    public class Remboussement : Controller
    {
        private readonly CmimDBContext context;

        public Remboussement(CmimDBContext dcontext)
        {
            context = dcontext;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Rembouse> Get()
        {
            return context.Remboursser;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRemboursement([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rembousse = context.Remboursser.Where(R => R.rembourssementId == id)
                .Select(R => new
                {
                    Rembourssemnt = R,
                    Dossiers = R.list.Select(L => L.Dossier)
                })
                .FirstOrDefault();

            if (rembousse == null)
            {
                return NotFound();
            }

           /* foreach(var l in rembousse.list)
            {
                
            } */

            return Ok(rembousse);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> PostRemboursement([FromBody] Rembouse rembouse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Remboursser.Add(rembouse);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetRemboursement", new { id = rembouse.rembourssementId }, rembouse);
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
