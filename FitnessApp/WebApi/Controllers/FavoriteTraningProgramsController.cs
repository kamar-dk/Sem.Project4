using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FA_DB.Data;
using FA_DB.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteTraningProgramsController : ControllerBase
    {
        private readonly DataContext _context;

        public FavoriteTraningProgramsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/FavoriteTraningPrograms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetfavoriteTraningPrograms()
        {
          if (_context.favoriteTraningPrograms == null)
          {
              return NotFound();
          }

          var programNames = _context.favoriteTraningPrograms.SelectMany(f => f.TraningPrograms).Select(p => p.Name);
          
          return await programNames.ToListAsync();
          //return await _context.favoriteTraningPrograms.ToListAsync();
        }

        // GET: api/FavoriteTraningPrograms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteTraningPrograms>> GetFavoriteTraningPrograms(string id)
        {
          if (_context.favoriteTraningPrograms == null)
          {
              return NotFound();
          }
            var favoriteTraningPrograms = await _context.favoriteTraningPrograms.FindAsync(id);

            if (favoriteTraningPrograms == null)
            {
                return NotFound();
            }

            return favoriteTraningPrograms;
        }

        //    // PUT: api/FavoriteTraningPrograms/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutFavoriteTraningPrograms(string id, FavoriteTraningPrograms favoriteTraningPrograms)
        //    {
        //        if (id != favoriteTraningPrograms.Email)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(favoriteTraningPrograms).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!FavoriteTraningProgramsExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // POST: api/FavoriteTraningPrograms
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<FavoriteTraningPrograms>> PostFavoriteTraningPrograms(FavoriteTraningPrograms favoriteTraningPrograms)
        //    {
        //      if (_context.favoriteTraningPrograms == null)
        //      {
        //          return Problem("Entity set 'DataContext.favoriteTraningPrograms'  is null.");
        //      }
        //        _context.favoriteTraningPrograms.Add(favoriteTraningPrograms);
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException)
        //        {
        //            if (FavoriteTraningProgramsExists(favoriteTraningPrograms.Email))
        //            {
        //                return Conflict();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return CreatedAtAction("GetFavoriteTraningPrograms", new { id = favoriteTraningPrograms.Email }, favoriteTraningPrograms);
        //    }

        //    // DELETE: api/FavoriteTraningPrograms/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteFavoriteTraningPrograms(string id)
        //    {
        //        if (_context.favoriteTraningPrograms == null)
        //        {
        //            return NotFound();
        //        }
        //        var favoriteTraningPrograms = await _context.favoriteTraningPrograms.FindAsync(id);
        //        if (favoriteTraningPrograms == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.favoriteTraningPrograms.Remove(favoriteTraningPrograms);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool FavoriteTraningProgramsExists(string id)
        //    {
        //        return (_context.favoriteTraningPrograms?.Any(e => e.Email == id)).GetValueOrDefault();
        //    }
    }
}
