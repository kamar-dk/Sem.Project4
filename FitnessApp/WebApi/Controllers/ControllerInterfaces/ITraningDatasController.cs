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
    public interface ITraningDatasController
    {

        // GET: api/TraningDatas
        [HttpGet]
        public Task<ActionResult<IEnumerable<TraningData>>> GettrantingData();

        // GET: api/TraningDatas/5
        [HttpGet("{id}")]
        public Task<ActionResult<TraningData>> GetTraningData(string id);

        // POST: api/TraningDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Task<ActionResult<TraningData>> PostTraningData(TraningDatasDto traningData);

        // DELETE: api/TraningDatas/5
        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteTraningData(string id);

    }
}
