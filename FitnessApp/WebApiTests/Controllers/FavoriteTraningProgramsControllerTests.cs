using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using WebApi.Data;
using WebApi.DTO;
using WebApi.Models;
using WebApi.Services;

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
        public async Task GetFavoriteTraningProgramsTest_ContextNull_Assert_OkObjectResult()
        {
            uut._context.favoriteTraningPrograms = null;
            var result = await uut.GetFavoriteTraningPrograms();
            var returned = result.Value;


            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            Assert.AreEqual(returned, null);
        }

        [Test()]
        public async Task GetFavoriteTraningProgramsTest_Success_Assert_ActionResultNotEmpty()
        {
            var result = await uut.GetFavoriteTraningPrograms();
            var returned = result.Value;


            Assert.That(result, Is.TypeOf<ActionResult<IEnumerable<FavoriteTraningPrograms>>>());
            Assert.That(returned, Is.Not.Empty);

        }

        [Test()]
        public void GetFavoriteTraningProgramsWithParamterTest_ProgramsNull_Assert_ActionResultEmpty()
        {
            var result = uut.GetFavoriteTraningPrograms("test@mail.dk");

            var returned = result.Result;

            Assert.That(result.Result, Is.TypeOf<ActionResult<IEnumerable<FavoriteTraningProgramsDto>>>());
            Assert.AreEqual(returned.Value, null);
        }

        [Test()]
        public void GetFavoriteTraningProgramsWithParamterTest_Success_Assert_ActionResult()
        {
            var result = uut.GetFavoriteTraningPrograms("asd@mail.dk");

            Assert.That(result.Result, Is.TypeOf<ActionResult<IEnumerable<FavoriteTraningProgramsDto>>>());
        }

        [Test()]
        public async Task PostFavoriteTraningProgramsTest_Success_Assert_ActionResultFTPDto()
        {
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
            await uut.DeletefavoriteTraningPrograms(ftpDto.Email, i.FavoriteTraningProgramsID);
        }

        [Test()]
        public async Task PostFavoriteTraningProgramTest_Assert_InvalidTraningprogramId()
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

        [Test()]
        public async Task DeletefavoriteTraningProgramsTest_Success_Assert_NoContentResult()
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
        }

        [Test()]
        public async Task DeletefavoriteTraningProgramsTest_Invaid_Assert_NoContentResult()
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
        public async Task PostFavoriteTraningProgramTest_Assert_ProgramAlreadyExist()
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

        [Test()]
        public async Task PutFavoriteTraningProgramsTest_IdAndDtoNotEqual_Assert_BadReguest()
        {
            var ftpDto = new FavoriteTraningProgramsDto()
            {
                FavoriteTraningProgramsID = 1,
                TraningProgramID = 1,
                Email = "bla"
            };

            var result = await uut.PutFavoriteTraningPrograms(-1, ftpDto);

            var value = result as BadRequestResult;

            Assert.That(result, Is.TypeOf<BadRequestResult>());
            Assert.AreEqual(400, value.StatusCode);
        }

        [Test()]
        public async Task PutFavoriteTraningProgramsTest_ContextNull_Assert_StatusCode404()
        {
            var ftpDto = new FavoriteTraningProgramsDto()
            {
                FavoriteTraningProgramsID = -1,
                Email = "test",
                TraningProgramID = 4,
                Name = "test"
            };

            //uut._context.favoriteTraningPrograms.FindAsync(ftpDto.Email).ReturnsNull();

            var result = await uut.PutFavoriteTraningPrograms(-1, ftpDto);

            var value = result as NotFoundResult;

            Assert.That(result, Is.TypeOf<NotFoundResult>());
            Assert.AreEqual(404, value.StatusCode);
        }

        [Test()]
        public async Task PutFavoriteTraningProgramsTest_TPidInvlid_Assert_InvalidTrainingProgramID()
        {
            var ftpDto = new FavoriteTraningProgramsDto()
            {
                FavoriteTraningProgramsID = 125,
                Email = "asd@mail.dk",
                TraningProgramID = -1,
                Name = "test"
            };

            var result = await uut.PutFavoriteTraningPrograms(125, ftpDto);

            var value = result as BadRequestObjectResult;

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.AreEqual(400, value.StatusCode);
            Assert.AreEqual("Invalid training program ID.", value.Value);
        }

        [Test()]
        public async Task PutFavoriteTraningProgramsTest_Success_Assert_NoContent()
        {
            var ftpDto = new FavoriteTraningProgramsDto()
            {
                FavoriteTraningProgramsID = 125,
                Email = "asd@mail.dk",
                TraningProgramID = 5,
                Name = "Legs"
            };

            var result = await uut.PutFavoriteTraningPrograms(125, ftpDto);

            var value = result as NoContentResult;

            Assert.That(result, Is.TypeOf<NoContentResult>());
            Assert.AreEqual(204, value.StatusCode);
        }
    }
}