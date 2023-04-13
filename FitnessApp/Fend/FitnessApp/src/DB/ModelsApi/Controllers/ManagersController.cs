using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ModelsApi.Data;
using ModelsApi.Models.DTOs;
using ModelsApi.Models.Entities;
using ModelsApi.Utilities;
using static BCrypt.Net.BCrypt;

namespace ModelsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public ManagersController(ApplicationDbContext context,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        // GET: api/Managers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EfManager>>> GetManagers()
        {
            return await _context.Managers.ToListAsync();
        }

        // GET: api/Managers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EfManager>> GetManager(long id)
        {
            var manager = await _context.Managers.FindAsync(id);

            if (manager == null)
            {
                return NotFound();
            }

            return manager;
        }

        // PUT: api/Managers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManager(long id, EfManager manager)
        {
            if (id != manager.EfManagerId)
            {
                return BadRequest();
            }

            // Check if new email
            var old = await _context.Managers.FindAsync(manager.EfManagerId);
            if (old.Email != manager.Email)
{
                // Update account
                var account = await _context.Accounts.FindAsync(manager.EfAccountId);
                account.Email = manager.Email;
            }

            _context.Entry(manager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagerExists(id))
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

        // POST: api/Managers
        [HttpPost]
        public async Task<ActionResult<EfManager>> PostManager(Manager managerDto)
        {
            if (managerDto == null)
                return BadRequest("Data is missing");
            var manager = new EfManager();
            manager.Email = managerDto.Email.ToLowerInvariant();
            var emailExist = await _context.Accounts.Where(u => u.Email == manager.Email)
                .FirstOrDefaultAsync().ConfigureAwait(false);
            if (emailExist != null)
            {
                ModelState.AddModelError("Email", "Email already in use");
                return BadRequest(ModelState);
            }
            manager.FirstName = managerDto.FirstName;
            manager.LastName = managerDto.LastName;
            var account = new EfAccount()
            {
                Email = manager.Email,
                IsManager = true
            };
            account.PwHash = HashPassword(managerDto.Password, _appSettings.BcryptWorkfactor);
            manager.Account = account;
            _context.Accounts.Add(account);
            _context.Managers.Add(manager);
            await _context.SaveChangesAsync();

            return Created(manager.EfManagerId.ToString(), manager);
        }

        // DELETE: api/Managers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EfManager>> DeleteManager(long id)
        {
            var manager = await _context.Managers.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(manager.EfAccountId);
            _context.Accounts.Remove(account);
            _context.Managers.Remove(manager);
            await _context.SaveChangesAsync();

            return manager;
        }

        private bool ManagerExists(long id)
        {
            return _context.Managers.Any(e => e.EfManagerId == id);
        }
    }
}
