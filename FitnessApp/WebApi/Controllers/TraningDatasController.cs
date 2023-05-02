using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FA_DB.Data;
using FA_DB.Models;
using AutoMapper;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraningDatasController : ControllerBase
    {
        private readonly DataContext _context;

        public TraningDatasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TraningDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TraningData>>> GettrantingData()
        {
          if (_context.traningData == null)
          {
              return NotFound();
          }
            return await _context.traningData.ToListAsync();
        }

        // GET: api/TraningDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TraningData>> GetTraningData(string id)
        {
          if (_context.traningData == null)
          {
              return NotFound();
          }
            var traningData = await _context.traningData.FindAsync(id);

            if (traningData == null)
            {
                return NotFound();
            }

            return traningData;
        }

        // PUT: api/TraningDatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraningData(string id, TraningDatasDto traningData)
        {
            if (id != traningData.Email)
            {
                return BadRequest();
            }

            _context.Entry(traningData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraningDataExists(id))
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

        // POST: api/TraningDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TraningData>> PostTraningData(TraningDatasDto traningData)
        {
          if (_context.traningData == null)
          {
              return Problem("Entity set 'DataContext.trantingData'  is null.");
          }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TraningDatasDto, TraningData>());
            var mapper = new Mapper(config);
            var user_ = mapper.Map<TraningData>(traningData);

            _context.traningData.Add(user_);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TraningDataExists(traningData.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTraningData", new { id = traningData.Email }, traningData);
        }

        // DELETE: api/TraningDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraningData(string id)
        {
            if (_context.traningData == null)
            {
                return NotFound();
            }
            var traningData = await _context.traningData.FindAsync(id);
            if (traningData == null)
            {
                return NotFound();
            }

            _context.traningData.Remove(traningData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TraningDataExists(string id)
        {
            return (_context.traningData?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
