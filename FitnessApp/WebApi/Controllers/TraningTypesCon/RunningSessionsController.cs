using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FA_DB.Data;
using FA_DB.Models.TraningTypes;

namespace WebApi.Controllers.TraningTypesCon
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunningSessionsController : ControllerBase
    {
        private readonly DataContext _context;

        public RunningSessionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/RunningSessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RunningSession>>> GetrunningSessions()
        {
          if (_context.runningSessions == null)
          {
              return NotFound();
          }
            return await _context.runningSessions.ToListAsync();
        }

        // GET: api/RunningSessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RunningSession>> GetRunningSession(int id)
        {
          if (_context.runningSessions == null)
          {
              return NotFound();
          }
            var runningSession = await _context.runningSessions.FindAsync(id);

            if (runningSession == null)
            {
                return NotFound();
            }

            return runningSession;
        }

        // PUT: api/RunningSessions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRunningSession(int id, RunningSession runningSession)
        {
            if (id != runningSession.SessionID)
            {
                return BadRequest();
            }

            _context.Entry(runningSession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RunningSessionExists(id))
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

        // POST: api/RunningSessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RunningSession>> PostRunningSession(RunningSession runningSession)
        {
          if (_context.runningSessions == null)
          {
              return Problem("Entity set 'DataContext.runningSessions'  is null.");
          }
            _context.runningSessions.Add(runningSession);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRunningSession", new { id = runningSession.SessionID }, runningSession);
        }

        // DELETE: api/RunningSessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRunningSession(int id)
        {
            if (_context.runningSessions == null)
            {
                return NotFound();
            }
            var runningSession = await _context.runningSessions.FindAsync(id);
            if (runningSession == null)
            {
                return NotFound();
            }

            _context.runningSessions.Remove(runningSession);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RunningSessionExists(int id)
        {
            return (_context.runningSessions?.Any(e => e.SessionID == id)).GetValueOrDefault();
        }
    }
}
