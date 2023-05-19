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

            Assert.AreEqual(6, value.Count()); // You need to look up how many traning programs there are in the database and then compare it to the value.StatusCode

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
                TraningProgramID = 3,
                Name = "Program 1"
            };

            var result = await uut.GetTraningProgram(tp.TraningProgramID);

            Assert.AreEqual(tp.TraningProgramID, result.Value.TraningProgramID);
        }

        [Test()]
        public void PutTraningProgramTest()
        {
            throw new NotImplementedException();
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

        [Test()]
        public async void PostTraningProgramTest()
        {
            throw new NotImplementedException();
        }


        [Test()]
        public void DeleteTraningProgramTest()
        {
            throw new NotImplementedException();
        }
    }
}