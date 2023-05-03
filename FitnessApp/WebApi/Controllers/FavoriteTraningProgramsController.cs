using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FA_DB.Data;

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
    }
}