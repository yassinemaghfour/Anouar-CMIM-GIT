using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMIM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cmim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityPlacesController : ControllerBase
    {
        private readonly CmimDBContext _context;

        public ActivityPlacesController(CmimDBContext context)
        {
            _context = context;
        }

        // GET: api/ActivityPlaces
        [HttpGet]
        public IEnumerable<ActivityPlace> GetActivityPlace()
        {
            return _context.ActivityPlace;
        }

        // GET: api/ActivityPlaces/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityPlace([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activityPlace = await _context.ActivityPlace.FindAsync(id);

            if (activityPlace == null)
            {
                return NotFound();
            }

            return Ok(activityPlace);
        }

        // PUT: api/ActivityPlaces/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityPlace([FromRoute] long id, [FromBody] ActivityPlace activityPlace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activityPlace.placdeId)
            {
                return BadRequest();
            }

            _context.Entry(activityPlace).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityPlaceExists(id))
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

        // POST: api/ActivityPlaces
        [HttpPost]
        public async Task<IActionResult> PostActivityPlace([FromBody] ActivityPlace activityPlace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ActivityPlace.Add(activityPlace);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivityPlace", new { id = activityPlace.placdeId }, activityPlace);
        }

        // DELETE: api/ActivityPlaces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityPlace([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activityPlace = await _context.ActivityPlace.FindAsync(id);
            if (activityPlace == null)
            {
                return NotFound();
            }

            _context.ActivityPlace.Remove(activityPlace);
            await _context.SaveChangesAsync();

            return Ok(activityPlace);
        }

        private bool ActivityPlaceExists(long id)
        {
            return _context.ActivityPlace.Any(e => e.placdeId == id);
        }
    }
}