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
using Mapster;

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
        
        /// <summary>
        /// Retrieves all training data.
        /// </summary>
        /// <returns>An ActionResult containing a list of TraningData.</returns>
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

        /// <summary>
        /// Retrieves a specific training data by its ID.
        /// </summary>
        /// <param name="id">The ID of the training data.</param>
        /// <returns>An ActionResult containing the requested TraningData.</returns>
        // GET: api/TraningDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TraningData>> GetTraningData(long id)
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

        //[HttpGet("Id/{UserId}")]
        //public async Task<ActionResult<IEnumerable<TraningData>>> GetTrainingDataEmail( string UserId)
        //{          
        //    var dbUserId = await _context.traningData.FindAsync(UserId);
        //    if(dbUserId == null)
        //    {
        //        return NotFound("UserId could not be found");
        //    }

        //    return Ok(UserId);


        //}


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


        /// <summary>
        /// Creates a new training data.
        /// </summary>
        /// <param name="traningData">The DTO representing the new training data.</param>
        /// <returns>An ActionResult containing the created TraningData.</returns>
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

        /// <summary>
        /// Deletes a specific training data by its ID.
        /// </summary>
        /// <param name="id">The ID of the training data.</param>
        /// <returns>An IActionResult indicating the result of the deletion operation.</returns>
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
