using AutoMapper;
using FA_DB.Data;
using FA_DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteTraningProgramsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FavoriteTraningProgramsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/FavoriteTraningPrograms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteTraningProgramsDto>>> GetFavoriteTraningPrograms()
        {
            var programs = await _context.favoriteTraningPrograms
                .Include(f => f.User)
                .Include(f => f.TraningPrograms)
                .ToListAsync();

            if (!programs.Any())
            {
                return NotFound();
            }

            var programDtos = _mapper.Map<List<FavoriteTraningProgramsDto>>(programs);

            return programDtos;
        }


        // GET: api/FavoriteTraningPrograms/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<FavoriteTraningProgramsDto>> GetFavoriteTraningPrograms(string email)
        {
            var program = await _context.favoriteTraningPrograms
                .Include(f => f.User)
                .Include(f => f.TraningPrograms)
                .FirstOrDefaultAsync(f => f.Email == email);

            if (program == null)
            {
                return NotFound();
            }

            AutoMapper.Mapper.Map<>

            var programDto = _mapper.Map<FavoriteTraningProgramsDto>(program);

            return programDto;
        }

        // POST: api/FavoriteTraningPrograms
        [HttpPost]
        public async Task<ActionResult<FavoriteTraningProgramsDto>> PostFavoriteTraningPrograms(FavoriteTraningProgramsDto programDto)
        {
            var program = _mapper.Map<FavoriteTraningPrograms>(programDto);

            if (program == null)
            {
                return BadRequest();
            }

            _context.favoriteTraningPrograms.Add(program);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFavoriteTraningPrograms), new { email = program.Email }, _mapper.Map<FavoriteTraningProgramsDto>(program));
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> PutFavoriteTraningPrograms(string email, FavoriteTraningProgramsDto programDto)
        {
            if (email != programDto.Email)
            {
                return BadRequest();
            }

            var program = await _context.favoriteTraningPrograms.FindAsync(email);
            if (program == null)
            {
                return NotFound();
            }

            _mapper.Map(programDto, program);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteTraningProgramsExists(email))
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

        // DELETE: api/FavoriteTraningPrograms/{email}
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeletefavoriteTraningPrograms(string email)
        {
            var program = await _context.favoriteTraningPrograms.FindAsync(email);
            if (program == null)
            {
                return NotFound();
            }

            _context.favoriteTraningPrograms.Remove(program);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoriteTraningProgramsExists(string email)
        {
            return _context.favoriteTraningPrograms.Any(e => e.Email == email);
        }

    }

}

//namespace WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FavoriteTraningProgramsController : ControllerBase
//    {
//        private readonly DataContext _context;

//        public FavoriteTraningProgramsController(DataContext context)
//        {
//            _context = context;
//        }

//        // GET: api/FavoriteTraningPrograms
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<string>>> GetfavoriteTraningPrograms()
//        {
//            var programNames = _context.favoriteTraningPrograms
//                .SelectMany(f => f.TraningPrograms)
//                .Select(p => p.Name);

//            return await programNames.ToListAsync();
//        }


//        [HttpPost]
//        public async Task<ActionResult<FavoriteTraningPrograms>> PostUserFavoriteProgram(FavoriteTraningProgramsDto userFTProgram)
//        {
//            if (_context.favoriteTraningPrograms == null)
//            {
//                return Problem("Entity set 'DataContext.userDatas'  is null.");
//            }
//            var config = new MapperConfiguration(cfg => cfg.CreateMap<FavoriteTraningProgramsDto, FavoriteTraningPrograms>());
//            var mapper = new Mapper(config);
//            var userFTProgram_ = mapper.Map<FavoriteTraningPrograms>(userFTProgram);

//            _context.favoriteTraningPrograms.Add(userFTProgram_);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (UserDataExists(userFTProgram.Email))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtAction("GetfavoriteTraningPrograms", new { id = userFTProgram.Email }, userFTProgram);
//        }

//        private bool UserDataExists(string id)
//        {
//            return (_context.favoriteTraningPrograms?.Any(e => e.Email == id)).GetValueOrDefault();
//        }

//    }
//}
