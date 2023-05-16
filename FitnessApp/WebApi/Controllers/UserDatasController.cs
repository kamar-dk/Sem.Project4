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
    public class UserDatasController : ControllerBase
    {
        private readonly DataContext _context;

        public UserDatasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/UserDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserData>>> GetuserDatas()
        {
          if (_context.userDatas == null)
          {
              return NotFound();
          }
            return await _context.userDatas.ToListAsync();
        }

        // GET: api/UserDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserData>> GetUserData(string id)
        {
          if (_context.userDatas == null)
          {
              return NotFound();
          }
            var userData = await _context.userDatas.FindAsync(id);

            if (userData == null)
            {
                return NotFound();
            }

            return userData;
        }

        // PUT: api/UserDatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserData(string id, UserDatasDto userDataDto)
        {
            if (id != userDataDto.Email)
            {
                return BadRequest();
            }
            var userData = await _context.userDatas.FindAsync(id);
            userData.Email = userDataDto.Email;
            userData.Height = userDataDto.Height;
            userData.Weight = userDataDto.Weight;
            userData.Gender = userDataDto.Gender;
            userData.DoB = userDataDto.DoB;

            _context.Entry(userData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDataExists(id))
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

        // POST: api/UserDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserData>> PostUserData(UserDatasDto userData)
        {
          if (_context.userDatas == null)
          {
              return Problem("Entity set 'DataContext.userDatas'  is null.");
          }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDatasDto, UserData>());
            var mapper = new Mapper(config);
            var userData_ = mapper.Map<UserData>(userData);

            _context.userDatas.Add(userData_);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserDataExists(userData.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserData", new { id = userData.Email }, userData);
        }

        // DELETE: api/UserDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserData(string id)
        {
            if (_context.userDatas == null)
            {
                return NotFound();
            }
            var userData = await _context.userDatas.FindAsync(id);
            if (userData == null)
            {
                return NotFound();
            }

            _context.userDatas.Remove(userData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDataExists(string id)
        {
            return (_context.userDatas?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
