using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraningDatasController : ControllerBase
    {
        private readonly DataContext _context;

        public TraningDatasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TraningDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TraningData>>> GettrantingData()
        {
          if (_context.trantingData == null)
          {
              return NotFound();
          }
            return await _context.trantingData.ToListAsync();
        }

        // GET: api/TraningDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TraningData>> GetTraningData(string id)
        {
          if (_context.trantingData == null)
          {
              return NotFound();
          }
            var traningData = await _context.trantingData.FindAsync(id);

            if (traningData == null)
            {
                return NotFound();
            }

            return traningData;
        }

        // PUT: api/TraningDatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraningData(string id, TraningData traningData)
        {
            if (id != traningData.Email)
            {
                return BadRequest();
            }

            _context.Entry(traningData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraningDataExists(id))
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

        // POST: api/TraningDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TraningData>> PostTraningData(TraningData traningData)
        {
          if (_context.trantingData == null)
          {
              return Problem("Entity set 'DataContext.trantingData'  is null.");
          }
            _context.trantingData.Add(traningData);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TraningDataExists(traningData.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTraningData", new { id = traningData.Email }, traningData);
        }

        // DELETE: api/TraningDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraningData(string id)
        {
            if (_context.trantingData == null)
            {
                return NotFound();
            }
            var traningData = await _context.trantingData.FindAsync(id);
            if (traningData == null)
            {
                return NotFound();
            }

            _context.trantingData.Remove(traningData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TraningDataExists(string id)
        {
            return (_context.trantingData?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
