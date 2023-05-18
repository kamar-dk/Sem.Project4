using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTO;
using WebApi.Models;
using WebApi.Data;
using WebApi.Controllers.ControllerInterfaces;

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

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<FavoriteTraningPrograms, FavoriteTraningProgramsDto>().ReverseMap();
                CreateMap<TraningPrograms, TraningProgramsDto>().ReverseMap();
            }
        }


        /// <summary>
        /// Retrieves a list of all favorite training programs.
        /// </summary>
        /// <returns>Gets a list of FavoriteTraningPrograms.</returns>
        // GET: api/FavoriteTraningPrograms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteTraningPrograms>>> GetFavoriteTraningPrograms()
        {
            if (_context.favoriteTraningPrograms == null)
            {
                return Ok(new List<FavoriteTraningProgramsDto>());
            }
            return await _context.favoriteTraningPrograms.ToListAsync();

            
        }




        /// <summary>
        /// Retrieves favorite training programs for a specific user identified by their email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>An ActionResult containing a list of FavoriteTraningProgramsDto.</returns>
        // GET: api/FavoriteTraningPrograms/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<FavoriteTraningProgramsDto>>> GetFavoriteTraningPrograms(string email)
        {
            var programs = await _context.favoriteTraningPrograms
                .Include(f => f.User)
                .Include(f => f.TraningProgram)
                .Where(f => f.Email== email)
                .ToListAsync();

            if (programs == null)
            {
                return Ok(new List<FavoriteTraningProgramsDto>());
            }

            var programDtos = _mapper.Map<IEnumerable<FavoriteTraningProgramsDto>>(programs);

            return Ok(programDtos);
        }

        /// <summary>
        /// Adds a new favorite training program.
        /// </summary>
        /// <param name="programDto">The DTO representing the new favorite training program.</param>
        /// <returns>An ActionResult containing the created FavoriteTraningProgramsDto.</returns>
        [HttpPost]
        public async Task<ActionResult<FavoriteTraningProgramsDto>> PostFavoriteTraningPrograms(FavoriteTraningProgramsDto programDto)
        {
            // Retrieve the training program based on the provided ID
            var trainingProgram = await _context.traningPrograms.FindAsync(programDto.TraningProgramID);
            if (trainingProgram == null)
            {
                return BadRequest("Invalid training program ID.");
            }

            // Create a new favorite training program object
            var favoriteProgram = new FavoriteTraningPrograms
            {
                Email = programDto.Email,
                TraningProgramID = programDto.TraningProgramID
            };

            // Add the favorite training program to the context
            _context.favoriteTraningPrograms.Add(favoriteProgram);
            await _context.SaveChangesAsync();

            // Map the created favorite training program to a DTO and return it
            var createdDto = _mapper.Map<FavoriteTraningProgramsDto>(favoriteProgram);
            return CreatedAtAction(nameof(GetFavoriteTraningPrograms), new { email = favoriteProgram.Email }, createdDto);
        }





        /// <summary>
        /// Updates an existing favorite training program.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="programDto">The DTO representing the updated favorite training program.</param>
        /// <returns>An IActionResult indicating the result of the update operation.</returns>
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


        /// <summary>
        /// Deletes a favorite training program based on the provided email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>An IActionResult indicating the result of the deletion operation.</returns>
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

        /// <summary>
        /// Checks if a favorite training program exists for the given email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>True if a favorite training program exists, otherwise false.</returns>
        private bool FavoriteTraningProgramsExists(string email)
        {
            return _context.favoriteTraningPrograms.Any(e => e.Email == email);
        }

    }

}


