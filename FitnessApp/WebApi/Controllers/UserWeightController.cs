//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using FA_DB.Data;
//using WebApi.DTO;
////using WebApi.Models;
//using FA_DB.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserWeightController : ControllerBase
//    {
//        private readonly DataContext _context;
//        private readonly IMapper _mapper;

//        public UserWeightController(DataContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }
//    }
//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<UserWeight>>> Getusers()
//    {
//        if (_context.users == null)
//        {
//            return NotFound();
//        }
//        return await _context.users.ToListAsync();

//    }

    