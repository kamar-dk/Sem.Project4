using NUnit.Framework;
using WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using WebApi.Data;
using WebApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.DTO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApi.Controllers.Tests
{
    [TestFixture()]
    public class FavoriteTraningProgramsControllerTests
    {

        DataContext mockedDbContext;

        public IUserServices _userServices = Substitute.For<IUserServices>();
        public DataContext _context;
        public FavoriteTraningProgramsController uut;
        public IMapper _mapper = Substitute.For<IMapper>();
        IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        IServiceCollection _services;
        private IServiceProvider? serviceProvider;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(@"Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=kaspermartensen_prj4nyny;User ID=kaspermartensen_prj4nyny;Password=123456;Encrypt=False; Trust Server Certificate=False;Persist Security Info = True;")
                .Options;

            mockedDbContext = new DataContext(contextOptions);
            _context = mockedDbContext;
            _mapper = Substitute.For<IMapper>();
            uut = new FavoriteTraningProgramsController(_context, _mapper);
        }

        [Test()]
        public async Task GetFavoriteTraningProgramsTest_ContextNull()
        {
            uut._context.favoriteTraningPrograms = null;
            var result = await uut.GetFavoriteTraningPrograms();

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test()]
        public async Task GetFavoriteTraningProgramsTest_Success()
        {
            var result = await uut.GetFavoriteTraningPrograms();

            Assert.That(result, Is.TypeOf<ActionResult<IEnumerable<FavoriteTraningPrograms>>>());
        }

        [Test()]
        public void GetFavoriteTraningProgramsWithParamterTest_ProgramsNull()
        {
            //uut._context.favoriteTraningPrograms.ToListAsync().Returns(null);
            var result = uut.GetFavoriteTraningPrograms("test@mail.dk");

            Assert.That(result.Result, Is.TypeOf<ActionResult<IEnumerable<FavoriteTraningProgramsDto>>>());
        }
        //asd@mail.dk

        [Test()]
        public void GetFavoriteTraningProgramsWithParamterTest_Success()
        {
            var result = uut.GetFavoriteTraningPrograms("asd@mail.dk");

            Assert.That(result.Result, Is.TypeOf<ActionResult<IEnumerable<FavoriteTraningProgramsDto>>>());
        }

        [Test()]
        public async Task PostFavoriteTraningProgramsTest()
        {
            //throw new NotImplementedException();

            var ftpDto = new FavoriteTraningProgramsDto()
            {
                FavoriteTraningProgramsID = 1,
                TraningProgramID = 6,
                Email = "test@mail.dk",
                Name = "Legs"
            };

            var result = uut.PostFavoriteTraningPrograms(ftpDto);
            var value = result.Result as ActionResult<FavoriteTraningProgramsDto>;

            Assert.That(result.Result, Is.TypeOf<ActionResult<FavoriteTraningProgramsDto>>());

            // Not  part of the test, just for cleanup
            var i = _context.favoriteTraningPrograms.ToList().Last();
            await uut.DeletefavoriteTraningPrograms(ftpDto.Email ,i.FavoriteTraningProgramsID);  
        }

        [Test()]
        public async Task PostFavoriteTraningProgramTest_InvalidTraningprogramId()
        {
            var ftpDto = new FavoriteTraningProgramsDto()
            {
                FavoriteTraningProgramsID = 1,
                TraningProgramID = 1,
                Email = "asd@mail.dk"
            };

            var result = await uut.PostFavoriteTraningPrograms(ftpDto);
            var value = result.Result as BadRequestObjectResult;

            Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
            Assert.That(value.Value, Is.EqualTo("Invalid training program ID."));
        }
        /*
        [Test()]
        public void PutFavoriteTraningProgramsTest()
        {
            throw new NotImplementedException();
        }*/

        [Test()]
        public async Task DeletefavoriteTraningProgramsTest_Success()
        {
            var ftpDto = new FavoriteTraningProgramsDto()
            {
                FavoriteTraningProgramsID = 1,
                TraningProgramID = 1,
                Email = "asd@mail.dk"
            };

            await uut.PostFavoriteTraningPrograms(ftpDto);
            

            var i = _context.favoriteTraningPrograms.ToList().Last();
            var result = await uut.DeletefavoriteTraningPrograms(ftpDto.Email, i.FavoriteTraningProgramsID);

            Assert.That(result, Is.TypeOf<NoContentResult>());

            //Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
            //Assert.That(value.Value, Is.EqualTo("Invalid training program ID."));
        }

        [Test()]
        public async Task DeletefavoriteTraningProgramsTest_Invaid()
        {
            var ftpdto = new FavoriteTraningProgramsDto()
            {
                FavoriteTraningProgramsID = 1,
                TraningProgramID = 1,
                Email = "test@mail.dk"
            };

            var result = await uut.DeletefavoriteTraningPrograms(ftpdto.Email, ftpdto.FavoriteTraningProgramsID);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test(), Order(1)]
        public async Task PostFavoriteTraningProgramTest_ProgramAlreadyExist()
        {
            var ftpDto = new FavoriteTraningProgramsDto()
            {
                FavoriteTraningProgramsID = 1,
                TraningProgramID = 4,
                Email = "asd@mail.dk"
            };

            var result = await uut.PostFavoriteTraningPrograms(ftpDto);
            var value = result.Result as BadRequestObjectResult;

            Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
            Assert.That(value.Value, Is.EqualTo("Program already exists"));
        }
    }
}