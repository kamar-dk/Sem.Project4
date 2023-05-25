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
