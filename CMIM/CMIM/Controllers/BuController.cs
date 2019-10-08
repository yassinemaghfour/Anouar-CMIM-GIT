using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMIM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMIM.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BuController : ControllerBase
    {
        private readonly CmimDBContext _context;


        public BuController(CmimDBContext context)
        {
            _context = context;
        }
        // GET: api/bu
        [HttpGet]
        public IEnumerable<BU> GetBu()
        {
            return _context.bu;
        }

        // GET: api/bu/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBu([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bu = await _context.bu.FindAsync(id);

            if (bu == null)
            {
                return NotFound();
            }

            return Ok(bu);
        }


        // PUT: api/bu/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBu([FromRoute] string id, [FromBody] BU bu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bu.BuId)
            {
                return BadRequest();
            }

            _context.Entry(bu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuExist(id))
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

        // POST: api/bu
        [HttpPost]
        public async Task<IActionResult> PostBu([FromBody] BU bu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.bu.Add(bu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBu", new { id = bu.BuId }, bu);
        }

        // DELETE: api/bu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBu([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bu = await _context.bu.FindAsync(id);
            if (bu == null)
            {
                return NotFound();
            }

            _context.bu.Remove(bu);
            await _context.SaveChangesAsync();

            return Ok(bu);
        }

        private bool BuExist(string id)
        {
            return _context.bu.Any(e => e.BuId == id);
        }
    }
  
}
