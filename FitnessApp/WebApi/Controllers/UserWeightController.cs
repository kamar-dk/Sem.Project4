using System.Collections.Generic;
using System.Threading.Tasks;
using FA_DB.Data;
using FA_DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWeightController : ControllerBase
    {
        private readonly DataContext _context;

        public UserWeightController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> Getusers()
        {
            var userWeights = await _context.UserWeights
                .Select(uw => new { Weight = uw.Weight, Date = uw.date })
                .ToListAsync();

            if (userWeights == null)
            {
                return NotFound();
            }

            return userWeights;
        }

    }
}