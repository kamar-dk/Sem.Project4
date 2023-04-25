using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models.TraningTypes;
using Mapster;


namespace WebApi.Controllers.TraningTypesCon
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeSessionsController : ControllerBase
    {
        private readonly DataContext _context;

        public BikeSessionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/BikeSessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeSession>>> GetbikeSessions()
        {
          if (_context.bikeSessions == null)
          {
              return NotFound();
          }
            return await _context.bikeSessions.ToListAsync();
        }

        // GET: api/BikeSessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BikeSession>> GetBikeSession(int id)
        {
          if (_context.bikeSessions == null)
          {
              return NotFound();
          }
            var bikeSession = await _context.bikeSessions.FindAsync(id);

            if (bikeSession == null)
            {
                return NotFound();
            }

            return bikeSession;
        }

        // PUT: api/BikeSessions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBikeSession(int id, BikeSession bikeSession)
        {
            if (id != bikeSession.BikeSessionId)
            {
                return BadRequest();
            }

            _context.Entry(bikeSession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BikeSessionExists(id))
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

        // POST: api/BikeSessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BikeSession>> PostBikeSession(BikeSession bikeSession)
        {
          if (_context.bikeSessions == null)
          {
              return Problem("Entity set 'DataContext.bikeSessions'  is null.");
          }
            Ok(_context.bikeSessions.Add(bikeSession.Adapt<BikeSession>()));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBikeSession", new { id = bikeSession.BikeSessionId }, bikeSession);
        }

        // DELETE: api/BikeSessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBikeSession(int id)
        {
            if (_context.bikeSessions == null)
            {
                return NotFound();
            }
            var bikeSession = await _context.bikeSessions.FindAsync(id);
            if (bikeSession == null)
            {
                return NotFound();
            }

            _context.bikeSessions.Remove(bikeSession);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BikeSessionExists(int id)
        {
            return (_context.bikeSessions?.Any(e => e.BikeSessionId == id)).GetValueOrDefault();
        }
    }
}
