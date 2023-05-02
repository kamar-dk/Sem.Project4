using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FA_DB.Data;
using FA_DB.Models;
using WebApi.DTO;
using AutoMapper;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraningProgramsController : ControllerBase
    {
        private readonly DataContext _context;

        public TraningProgramsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TraningPrograms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TraningProgram>>> GettraningPrograms()
        {
          if (_context.traningPrograms == null)
          {
              return NotFound();
          }
            return await _context.traningPrograms.ToListAsync();
        }

        // GET: api/TraningPrograms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TraningProgram>> GetTraningProgram(int id)
        {
          if (_context.traningPrograms == null)
          {
              return NotFound();
          }
            var traningProgram = await _context.traningPrograms.FindAsync(id);

            if (traningProgram == null)
            {
                return NotFound();
            }

            return traningProgram;
        }

        // PUT: api/TraningPrograms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraningProgram(int id, TraningProgram traningProgram)
        {
            if (id != traningProgram.TraningProgramID)
            {
                return BadRequest();
            }

            _context.Entry(traningProgram).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraningProgramExists(id))
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

        // POST: api/TraningPrograms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TraningProgram>> PostTraningProgram(TraningProgramsDto traningProgram)
        {
          if (_context.traningPrograms == null)
          {
              return Problem("Entity set 'DataContext.traningPrograms'  is null.");
          }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TraningProgramsDto, TraningProgram>());
            var mapper = new Mapper(config);
            var program = mapper.Map<TraningProgram>(traningProgram);

            _context.traningPrograms.Add(program);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TraningProgramExists(traningProgram.TraningProgramID))
                {
                    return Conflict();
                }
                else    
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = traningProgram.TraningProgramID }, traningProgram);
        }

        // DELETE: api/TraningPrograms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraningProgram(int id)
        {
            if (_context.traningPrograms == null)
            {
                return NotFound();
            }
            var traningProgram = await _context.traningPrograms.FindAsync(id);
            if (traningProgram == null)
            {
                return NotFound();
            }

            _context.traningPrograms.Remove(traningProgram);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TraningProgramExists(int id)
        {
            return (_context.traningPrograms?.Any(e => e.TraningProgramID == id)).GetValueOrDefault();
        }

    }
}
