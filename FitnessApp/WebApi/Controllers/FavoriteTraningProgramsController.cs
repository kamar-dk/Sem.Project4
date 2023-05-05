using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FA_DB.Data;
using AutoMapper;
using FA_DB.Models;
using WebApi.DTO;

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
            var programNames = _context.favoriteTraningPrograms
                .SelectMany(f => f.TraningPrograms)
                .Select(p => p.Name);

            return await programNames.ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<FavoriteTraningPrograms>> PostUserData(FavoriteTraningProgramsDto userFTProgram)
        {
            if (_context.favoriteTraningPrograms == null)
            {
                return Problem("Entity set 'DataContext.userDatas'  is null.");
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<FavoriteTraningProgramsDto, FavoriteTraningPrograms>());
            var mapper = new Mapper(config);
            var userFTProgram_ = mapper.Map<FavoriteTraningPrograms>(userFTProgram);

            _context.favoriteTraningPrograms.Add(userFTProgram_);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserDataExists(userFTProgram.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetfavoriteTraningPrograms", new { id = userFTProgram.Email }, userFTProgram);
        }

        private bool UserDataExists(string id)
        {
            return (_context.favoriteTraningPrograms?.Any(e => e.Email == id)).GetValueOrDefault();
        }

    }
}
