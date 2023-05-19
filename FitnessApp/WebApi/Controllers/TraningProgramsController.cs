using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTO;
using AutoMapper;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraningProgramsController : ControllerBase
    {
        public readonly DataContext _context;

        public TraningProgramsController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a collection of training programs.
        /// </summary>
        /// <returns>An ActionResult containing a collection of TraningPrograms objects.</returns>
        // GET: api/TraningPrograms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TraningPrograms>>> GettraningPrograms()
        {
          if (_context.traningPrograms == null)
          {
              return NotFound();
          }
            return await _context.traningPrograms.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific training program by ID.
        /// </summary>
        /// <param name="id">The ID of the training program to retrieve.</param>
        /// <returns>An ActionResult containing the TraningPrograms object.</returns>
        // GET: api/TraningPrograms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TraningPrograms>> GetTraningProgram(int id)
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

        /// <summary>
        /// Updates a training program with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the training program to update.</param>
        /// <param name="traningProgram">The TraningPrograms object containing the updated training program data.</param>
        /// <returns>An IActionResult representing the result of the update operation.</returns>
        // PUT: api/TraningPrograms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraningProgram(int id, TraningPrograms traningProgram)
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

        /// <summary>
        /// Creates a new training program.
        /// </summary>
        /// <param name="traningProgram">The TraningProgramsDto object containing the training program data to create.</param>
        /// <returns>An ActionResult containing the created TraningPrograms object.</returns>
        // POST: api/TraningPrograms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TraningPrograms>> PostTraningProgram(TraningProgramsDto traningProgram)
        {
          if (_context.traningPrograms == null)
          {
              return Problem("Entity set 'DataContext.traningPrograms'  is null.");
          }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TraningProgramsDto, TraningPrograms>());
            var mapper = new Mapper(config);
            var program = mapper.Map<TraningPrograms>(traningProgram);

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

        /// <summary>
        /// Deletes a training program with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the training program to delete.</param>
        /// <returns>An IActionResult representing the result of the delete operation.</returns>
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
