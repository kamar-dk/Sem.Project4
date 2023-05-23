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

namespace WebApi.Controllers.Tests
{
    [TestFixture()]
    public class TraningProgramsControllerTests
    {

        DataContext mockedDbContext;

        public IUserServices _userServices = Substitute.For<IUserServices>();
        public DataContext _context;
        public TraningProgramsController uut;
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
            uut = new TraningProgramsController(_context);
        }

        [Test()]
        public async Task GettraningProgramsTest_ContextNullAsync()
        {
            uut._context.traningPrograms = null;

            var result = await uut.GettraningPrograms();
            var value = result.Result as NotFoundResult;

            Assert.AreEqual(404, value.StatusCode);
        }

        [Test()]
        public async Task GettraningProgramsTest_success()
        {
            var result = await uut.GettraningPrograms();
            var value = result.Value;

            Assert.AreEqual(5, value.Count()); // You need to look up how many traning programs there are in the database and then compare it to the value.StatusCode

        }

        [Test()]
        public async Task GetTraningProgramTest_ContextTraningProgramsNull()
        {
            //Arrange
            uut._context.traningPrograms = null;

            var tp = new TraningPrograms()
            {
                TraningProgramID = 1,
                Name = "Test"
            };

            var result = await uut.GetTraningProgram(tp.TraningProgramID);
            var value = result.Result as NotFoundResult;

            Assert.AreEqual(404, value.StatusCode);
        }

        [Test()]
        public async Task GetTraningProgramTest_TraningprogramsNull()
        {
            //Arrange

            var tp = new TraningPrograms()
            {
                TraningProgramID = -1,
                Name = "Test"
            };

            var result = await uut.GetTraningProgram(tp.TraningProgramID);
            var value = result.Result as NotFoundResult;

            Assert.AreEqual(404, value.StatusCode);
        }

        [Test()]
        public async Task GetTraningProgramTest_Success()
        {
            //Arrange

            var tp = new TraningPrograms()
            {
                TraningProgramID = 4,
                Name = "Chest"
            };

            var result = await uut.GetTraningProgram(tp.TraningProgramID);

            Assert.AreEqual(tp.TraningProgramID, result.Value.TraningProgramID);
        }

        /*
        [Test()]
        public async Task PutTraningProgramTest()
        {
            throw new NotImplementedException();
        }*/
        /*
        [Test()]
        public async Task PutTraningProgramTest_IdsNotEqual_AssertBadrequest()
        {
            var tp = new TraningPrograms()
            {
                TraningProgramID = 1,
                Name = "Test"
            };

            var result = await uut.PutTraningProgram(2, tp);

            var value = result as BadRequestResult;

            Assert.AreEqual(400, value.StatusCode);
        }



        [Test()]
        public async Task PostTraningProgramTest_ContextNull()
        {
            uut._context.traningPrograms = null;

            var tp = new TraningProgramsDto()
            {
                TraningProgramID = 1,
                Name = "Test"
            };

            var result = await uut.PostTraningProgram(tp);
            var value = result.Result as ObjectResult;
            var status = value.Value as ProblemDetails;

            Assert.AreEqual("Entity set 'DataContext.traningPrograms'  is null.", status.Detail);
        }
        /* Can't post with Id throws exception
        [Test()]
        public async Task PostTraningProgramTest_ProgramExcistExpectConflict()
        {
            var tp = new TraningProgramsDto()
            {
                TraningProgramID = 3,
                Name = "Program 1"
            };

            var result = await uut.PostTraningProgram(tp);
            var value = result.Result as ConflictResult;
            
            Assert.AreEqual(409, value.StatusCode);
        }*/
        /*
        [Test()]
        public async Task PostTraningProgram_UpdateException()
        {
            var tp = new TraningProgramsDto()
            {
                TraningProgramID = 1,
                Name = "Test"
            };

            Assert.ThrowsAsync<DbUpdateException>(() => uut.PostTraningProgram(tp));
        }*/
        /*
        [Test()]
        public async Task PostTraningProgramTest_Success()
        {
            var tp = new TraningProgramsDto()
            {
                Name = "Program 1 test"
            };

            var result = await uut.PostTraningProgram(tp);

            
            //var newtp = result.;
            var value = result.Result as CreatedAtActionResult;

            Assert.AreEqual(201, value.StatusCode);

            var newid = value.ActionName;
            var idint = Int32.Parse(newid);

            await uut.DeleteTraningProgram(idint);
        }
        */

        [Test()]
        public async Task DeleteTraningProgramTest_ContextNull()
        {
            uut._context.traningPrograms = null;
            var result = await uut.DeleteTraningProgram(1);

            var value = result as NotFoundResult;

            Assert.AreEqual(404, value.StatusCode);

        }

        [Test()]
        public async Task DeleteTraningProgramTest_TraningProgramNull()
        {
            var result = await uut.DeleteTraningProgram(-1);

            var value = result as NotFoundResult;

            Assert.AreEqual(404, value.StatusCode);
        }
        /*
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Test()]
        public async Task DeleteTraningProgramTest_Success()
        {
            var tp = new TraningProgramsDto()
            {
                Name = "Program 1 test"
            };

            var result1 = await uut.PostTraningProgram(tp);
            var value1 = result1.Result as CreatedAtActionResult;
            var newid = value1.ActionName;
            var idint = Int32.Parse(newid);

            var result = await uut.DeleteTraningProgram(idint);
            var value = result as NoContentResult;

            Assert.AreEqual(204, value.StatusCode);


        }*/
    }
}