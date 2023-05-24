using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using WebApi.Data;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers.Tests
{
    [TestFixture()]
    public class TraningProgramsControllerTests
    {

        DataContext mockedDbContext;

        //public IUserServices _userServices = Substitute.For<IUserServices>();
        public DataContext _context;
        public TraningProgramsController uut;
        public IMapper _mapper = Substitute.For<IMapper>();
        IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(@"Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=kaspermartensen_prj4nyny;User ID=kaspermartensen_prj4nyny;Password=123456;Encrypt=False; Trust Server Certificate=False;Persist Security Info = True;")
                .Options;

            _context = new DataContext(contextOptions);
            _mapper = Substitute.For<IMapper>();
            uut = new TraningProgramsController(_context);
        }

        [Test()]
        public async Task GettraningProgramsTest_ContextNull_AssertStatusCode404()
        {
            uut._context.traningPrograms = null;

            var result = await uut.GettraningPrograms();
            var value = result.Result as NotFoundResult;

            Assert.AreEqual(404, value.StatusCode);
        }

        [Test()]
        public async Task GettraningProgramsTest_success_Assert_GettingTraningProgramsAmount()
        {
            var result = await uut.GettraningPrograms();
            var value = result.Value;

            Assert.AreEqual(5, value.Count()); 

        }

        [Test()]
        public async Task GetTraningProgramTest_ContextTraningProgramsNull_Assert_StatusCode404()
        {
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
        public async Task GetTraningProgramTest_TraningprogramsNull_Assert_StatusCode404()
        {
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
        public async Task GetTraningProgramTest_Success_Assert_TpAndResultIdEqual()
        {
            var tp = new TraningPrograms()
            {
                TraningProgramID = 4,
                Name = "Chest"
            };

            var result = await uut.GetTraningProgram(tp.TraningProgramID);

            Assert.AreEqual(tp.TraningProgramID, result.Value.TraningProgramID);
        }

        [Test()]
        public async Task DeleteTraningProgramTest_ContextNull_Assert_StatusCode404()
        {
            uut._context.traningPrograms = null;
            var result = await uut.DeleteTraningProgram(1);

            var value = result as NotFoundResult;

            Assert.AreEqual(404, value.StatusCode);

        }

        [Test()]
        public async Task DeleteTraningProgramTest_TraningProgramNull_Assert_StatusCode404()
        {
            var result = await uut.DeleteTraningProgram(-1);

            var value = result as NotFoundResult;

            Assert.AreEqual(404, value.StatusCode);
        }
    }
}