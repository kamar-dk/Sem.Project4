using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FA_DB.Data;
using WebApi.DTO;
//using WebApi.Models;
using WebApi.Services;
using FA_DB.Models;
using System.Configuration;
using Mapster;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserServices _accountServices;

        //private readonly IMapper _mapper;

        public UsersController(DataContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _accountServices = new UserServices(configuration);

        }


        //[AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserRegisterDto register)
        {
            if (!_accountServices.IsVaildEmail(register.Email))
            {
                return BadRequest("Email is not valid");
            }

            if (await _context.users.AnyAsync(x => x.Email == register.Email))
                return BadRequest("Email is already taken");

            _accountServices.CreatePasswordHash(register.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var account = new User
            {
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
                PasswordHash = passwordHash,
                Salt = passwordSalt,
            };
            //_context.Calender.Add(Calender);

            var userData = new UserData
            {
                Email = register.Email
            };


            _context.users.Add(account);
            _context.userDatas.Add(userData);

            await _context.SaveChangesAsync();

            return Accepted(register.Email);
        }


        //[AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserLoginDto request)
        {
            var dbAccount = await _context.users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (dbAccount == null)
            {
                return NotFound(request.Email);
            }

            if (!_accountServices.TryVerifyPasswordHash(request.Password, dbAccount.PasswordHash, dbAccount.Salt))
            {
                return BadRequest("Not a valid Password");
            }

            var token = _accountServices.CreateToken(dbAccount);
            var account = dbAccount.Adapt<UserDto>();
            account.Token = token;
            return Ok(account);

        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Getusers()
        {
          if (_context.users == null)
          {
              return NotFound();
          }
            return await _context.users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
          if (_context.users == null)
          {
              return NotFound();
          }
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, UserDto userDto)
        {
            if (id != userDto.Email)
            {
                return BadRequest();
            }

            var user = await _context.users.FindAsync(id);
            user.Email = userDto.Email;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.PasswordHash = userDto.PasswordHash;
            user.Salt = userDto.Salt;

            //_mapper.Map(userDto, user);

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        //// POST: api/Users
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUserDto(UserDto user)
        //{
        //    if (_context.users == null)
        //    {
        //        return Problem("Entity set 'DataContext.users'  is null.");
        //    }
        //    var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDto, User>());
        //    var mapper = new Mapper(config);
        //    var user_ = mapper.Map<User>(user);

        //    _context.users.Add(user_);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (UserExists(user.Email))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetUser", new { id = user.Email }, user);
        //}


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (_context.users == null)
            {
                return NotFound();
            }
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return (_context.users?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
