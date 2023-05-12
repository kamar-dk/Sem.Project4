using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserWeightController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserWeight, UserWeightDto>();
                cfg.CreateMap<UserWeightDto, UserWeight>();
            });
            _mapper = config.CreateMapper();
        }

        [HttpGet]
        public IEnumerable<UserWeightDto> GetUsers()
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

        [HttpPost]
        public async Task<ActionResult<UserWeightDto>> CreateUserWeight([FromBody] UserWeightDto userWeightDto)
        {
            if (userWeightDto == null)
            {
                return BadRequest("Invalid user weight data");
            }

            var userData = await _context.userDatas
                .SingleOrDefaultAsync(ud => ud.Email == userWeightDto.UserData.Email);

            if (userData == null)
            {
                return BadRequest("Invalid user data");
            }

            var newUserWeight = _mapper.Map<UserWeight>(userWeightDto);

            _context.UserWeights.Add(newUserWeight);
            await _context.SaveChangesAsync();

            var createdUserWeightDto = _mapper.Map<UserWeightDto>(newUserWeight);

            return CreatedAtAction(nameof(GetUserWeight), new { id = createdUserWeightDto.UserData.User.Email }, createdUserWeightDto);

        }


        [HttpPut("{id}")]
        public async Task<ActionResult<UserWeightDto>> UpdateUserWeight(int id, [FromBody] UserWeightDto userWeightDto)
        {
            var userWeightToUpdate = await _context.UserWeights
                .Include(uw => uw.UserData)
                .SingleOrDefaultAsync(uw => uw.ID == id);

            if (userWeightToUpdate == null)
            {
                return NotFound();
            }

            _mapper.Map(userWeightDto, userWeightToUpdate);

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
    
        

        //private bool createdUserWeightDto(int id)
        //{
        //    return _context.UserWeights.Any(uw => uw.ID == id);
        //}

    }
}
