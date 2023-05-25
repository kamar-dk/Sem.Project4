//Create interface for TraningDatasController
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Controllers.ControllerInterfaces
{
    public interface ITrainingDatasController
    {

        // GET: api/TraningDatas
        [HttpGet]
        public Task<ActionResult<IEnumerable<TrainingData>>> GetTrainingData();

        // GET: api/TraningDatas/5
        [HttpGet("{id}")]
        public Task<ActionResult<TrainingData>> GetTrainingData(long id);

        // Get: api/TraningDatas/Email
        //[HttpGet("{Userid}")]
        //public Task<ActionResult<IEnumerable<TraningData>>> GetTrainingDataEmail(string UserId);


        // POST: api/TraningDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Task<ActionResult<TrainingData>> PostTrainingData(TrainingDatasDto traningData);

        // DELETE: api/TraningDatas/5
        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteTrainingData(long id);
                

    }
}
