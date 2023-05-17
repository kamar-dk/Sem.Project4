using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mapster;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTO;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Controllers.ControllerInterfaces
{
    public interface IFavoriteTraningProgramsController
    {
        public Task<ActionResult<IEnumerable<FavoriteTraningProgramsDto>>> GetFavoriteTraningPrograms();

        public Task<ActionResult<FavoriteTraningProgramsDto>> GetFavoriteTraningPrograms(string email);

        public Task<ActionResult<FavoriteTraningProgramsDto>> PostFavoriteTraningPrograms(FavoriteTraningProgramsDto programDto);

        public Task<IActionResult> PutFavoriteTraningPrograms(string email, FavoriteTraningProgramsDto programDto);

        public Task<IActionResult> DeletefavoriteTraningPrograms(string email);


        //private bool FavoriteTraningProgramsExists(string email);
    }
}
