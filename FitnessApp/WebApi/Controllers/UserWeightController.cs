using System.Collections.Generic;
using System.Threading.Tasks;
using FA_DB.Data;
using FA_DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTO;

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
        public async Task<ActionResult<IEnumerable<object>>> GetUsers()
        {
            var userWeights = await _context.UserWeights
                .Select(uw => new { Weight = uw.Weight, Date = uw.date })
                .ToListAsync();

            return userWeights;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserWeightDto>> GetUserWeight(int id)
        {
            var userWeight = await _context.UserWeights.FindAsync(id);

            if (userWeight == null)
            {
                return NotFound();
            }

            var userWeightDto = new UserWeightDto
            {
                Weight = userWeight.Weight,
                Date = userWeight.date
            };

            return userWeightDto;
        }


        [HttpPost]
        public async Task<ActionResult<UserWeightDto>> CreateUserWeight(UserWeightDto userWeightDto)
        {
            var userDataEmail = User.Identity.Name;
            var userData = await _context.userDatas.SingleOrDefaultAsync(u => u.Email == userDataEmail);

            var newUserWeight = new UserWeight
            {
                Weight = userWeightDto.Weight,
                date = userWeightDto.Date,
                UserData = userData
            };

            _context.UserWeights.Add(newUserWeight);
            await _context.SaveChangesAsync();

            var createdUserWeightDto = new UserWeightDto
            {
                Weight = newUserWeight.Weight,
                Date = newUserWeight.date
            };

            return CreatedAtAction(nameof(GetUserWeight), new { id = newUserWeight.ID }, createdUserWeightDto);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserWeight(int id, UserWeightDto userWeightDto)
        {
            var userWeightToUpdate = await _context.UserWeights.FindAsync(id);

            if (userWeightToUpdate == null)
            {
                return NotFound();
            }

            userWeightToUpdate.Weight = userWeightDto.Weight;
            userWeightToUpdate.date = userWeightDto.Date;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserWeightExists(id))
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

        private bool UserWeightExists(int id)
        {
            return _context.UserWeights.Any(uw => uw.ID == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserWeightDto>> DeleteUserWeight(int id)
        {
            var userWeightToDelete = await _context.UserWeights.FindAsync(id);

            if (userWeightToDelete == null)
            {
                return NotFound();
            }

            var deletedUserWeight = new UserWeightDto
            {
                Weight = userWeightToDelete.Weight,
                Date = userWeightToDelete.date
            };

            _context.UserWeights.Remove(userWeightToDelete);
            await _context.SaveChangesAsync();

            return deletedUserWeight;
        }

    }
}
