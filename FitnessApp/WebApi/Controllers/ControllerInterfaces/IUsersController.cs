//Create interface for UsersController
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Controllers.ControllerInterfaces
{
    public interface IUsersController
    {
        
        Task<ActionResult<UserDto>> Register(UserRegisterDto register);
        Task<ActionResult<UserDto>> Login(UserLoginDto login);
        ActionResult<IEnumerable<UserDto>> Getusers();
        ActionResult<UserDto> GetUser(int id);

    }
}
