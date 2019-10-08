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
    public class SiteController : ControllerBase
    {
        private readonly CmimDBContext _context;

        public SiteController(CmimDBContext context)
        {
            _context = context;
        }


        // GET: api/Site
        [HttpGet]
        public IEnumerable<Site> GetSite()
        {
            return _context.sites;
        }

        // GET: api/Site/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSite([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var site = await _context.sites.FindAsync(id);

            if (site == null)
            {
                return NotFound();
            }

            return Ok(site);
        }


        // PUT: api/Site/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSite([FromRoute] long id, [FromBody] Site site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != site.SiteId)
            {
                return BadRequest();
            }

            _context.Entry(site).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(id))
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
        // POST: api/Site
        [HttpPost]
        public async Task<IActionResult> PostActivityPlace([FromBody] Site site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.sites.Add(site);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSite", new { id = site.SiteId }, site);
        }
        // DELETE: api/ActivityPlaces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSite([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var site = await _context.sites.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }

            _context.sites.Remove(site);
            await _context.SaveChangesAsync();

            return Ok(site);
        }

        private bool SiteExists(long id)
        {
            return _context.sites.Any(e => e.SiteId == id);
        }
    }
}
