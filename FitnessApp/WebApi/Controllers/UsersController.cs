using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTO;
//using WebApi.Models;
using WebApi.Services;
using System.Configuration;
using Mapster;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public DataContext _context;
        public IUserServices _accountServices;

        private readonly IMapper _mapper;

        public UsersController(DataContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _accountServices = new UserServices(configuration);

        }


        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="register">The DTO containing user registration data.</param>
        /// <returns>An ActionResult containing the registered UserDto.</returns>
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
                Gender = register.Gender,
               PasswordHash = passwordHash,
                Salt = passwordSalt
            };
            //_context.Calender.Add(Calender);

            var userData = new UserData
            {
                Email = register.Email,
                Height = register.Height,
                Weight = register.Weight
            };


            _context.users.Add(account);
            _context.userDatas.Add(userData);

            await _context.SaveChangesAsync();

            return Accepted(register.Email);
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="request">The DTO containing user login data.</param>
        /// <returns>An ActionResult containing the logged-in UserDto with a token.</returns>
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
            return Ok(account.Token);

        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>An ActionResult containing a list of User.</returns>
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

        /// <summary>
        /// Retrieves a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>An ActionResult containing the requested User.</returns>
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

        [HttpGet("{id}/TraningData")]
        public async Task<ActionResult<UserDto>> GetTrainingsData(string id)
        {
            var trainingData = await _context.users.FindAsync(id);
            if(trainingData == null)
            {
                return NotFound("Training data could not be found");
            }

            _context.Entry(trainingData).Collection(i => i.TraningDatas).Load();

            var trainingDataWithEmail = trainingData.Adapt<UserDto>();

            foreach (var data in trainingData.TraningDatas) 
            {
                var training = new TraningDatasDto
                {
                    Id = data.Id,
                    UserId = data.UserId,
                    TrainingType = data.TrainingType,
                    SessionDate = data.SessionDate,
                    Distance = data.Distance,
                    SessionHourTime = data.SessionHourTime,
                    SessionMinuteTime = data.SessionMinuteTime,
                    SessionSecondTime = data.SessionSecondTime,
                    Calories = data.Calories,
                    MaxHeartRate = data.MaxHeartRate,
                    MinHeartRate = data.MinHeartRate,
                    AvgHeartRate = data.AvgHeartRate,
                    Vo2Max = data.Vo2Max
                };
                trainingDataWithEmail.TraningDatasDtos.Add(training);
            }
            return Ok(trainingDataWithEmail);

        }

        /// <summary>
        /// Updates a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="userDto">The updated UserDto.</param>
        /// <returns>An IActionResult indicating the result of the update operation.</returns>
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


        /// <summary>
        /// Deletes a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>An IActionResult indicating the result of the deletion operation.</returns>
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

        /// <summary>
        /// Checks if a user with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>True if the user exists, otherwise false.</returns>
        private bool UserExists(string id)
        {
            return (_context.users?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
