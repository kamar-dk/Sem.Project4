using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTO;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWeightController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserWeightController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserWeight, UserWeightDto>();
                cfg.CreateMap<UserWeightDto, UserWeight>();
            });

            _mapper = config.CreateMapper();
        }

        [HttpGet]
        public IEnumerable<UserWeightDto> GetUser()
        {
            var userWeights = _context.UserWeights
                .Include(uw => uw.UserData)
                .Select(uw => _mapper.Map<UserWeightDto>(uw))
                .ToList();

            return userWeights;
        }

        [HttpGet("{id}")]
        public ActionResult<UserWeightDto> GetUserWeight(int id)
        {
            var userWeight = _context.UserWeights
                .Include(uw => uw.UserData)
                .SingleOrDefault(uw => uw.ID == id);

            if (userWeight == null)
            {
                return NotFound();
            }

            var userWeightDto = _mapper.Map<UserWeightDto>(userWeight);

            return userWeightDto;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserWeightDto>> UpdateUserWeight(int id, [FromBody] UserWeightDto userWeightDto)
        {
            var userWeightToUpdate = await _context.UserWeights
                .Include(uw => uw.UserData)
                .SingleOrDefaultAsync(uw => uw.ID == id);

            if (userWeightToUpdate == null)
            {
                return NotFound("User weight not found");
            }

            userWeightToUpdate.UserData.Weight = userWeightDto.Weight; // Update current weight

            var newWeight = new UserWeight
            {
                Weight = userWeightDto.Weight,
                date = userWeightDto.Date,
                UserData = userWeightToUpdate.UserData
            };

            _context.UserWeights.Add(newWeight); // Add new weight to the list

            await _context.SaveChangesAsync();

            var updatedUserWeightDto = _mapper.Map<UserWeightDto>(userWeightToUpdate);

            return updatedUserWeightDto;
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserWeight(int id)
        {
            var userWeightToDelete = await _context.UserWeights.FindAsync(id);

            if (userWeightToDelete == null)
            {
                return NotFound();
            }

            _context.UserWeights.Remove(userWeightToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
