using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApi.DTO;
using WebApi.Models;
using WebApi.Data;

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
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTraningData(long id, TraningDatasDto traningData)
        //{
        //    if (id != traningData.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(traningData).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TraningDataExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/TraningDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TraningData>> PostTraningData(TraningDatasDto traningData)
        {
          if (_context.traningData == null)
          {
              return Problem("Entity set 'DataContext.trainingData'  is null.");
          }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TraningDatasDto, TraningData>());
            var mapper = new Mapper(config);
            var user_ = mapper.Map<TraningData>(traningData);
            var us = _context.users.Where(u => u.Email == traningData.UserId).FirstOrDefault();
            user_.User = us;

            _context.traningData.Add(user_);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TraningDataExists(traningData.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTraningData", new { id = traningData.Id }, traningData);
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

        private bool TraningDataExists(long id)
        {
            return (_context.traningData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
