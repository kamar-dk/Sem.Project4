using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using WebApi.Data;
using WebApi.DTO;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers.Tests
{
    [TestFixture()]
    public class UsersControllerTests
    {
        DataContext mockedDbContext;

        public IUserServices _userServices = Substitute.For<IUserServices>();
        public DataContext _context;
        public UsersController uut;
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
            uut = new UsersController(_context, _mapper, _configuration);
        }

        [Test()]
        public async Task RegisterTest_Assert_InvalidEmailAsync(/*string Expected*/)
        {
            //Arrange
            var register = new UserRegisterDto
            {
                Email = "test",
                FirstName = "Test",
                LastName = "Test",
                Password = "Test"
            };
            //uut._accountServices.IsValidEmail(register.Email).Returns(true);
            //uut._context.users.AnyAsync(x => x.Email == register.Email).Returns(true);
            var result = await uut.Register(register);
            var value = result.Result as BadRequestObjectResult;

            Assert.AreEqual(value.Value, "Email is not valid");
        }


        [TestCase("Email is already taken")]
        public async Task RegisterTest_Assert_EmailAlreadyTakenAsync(string Expected)
        {
            //Arrange
            var register = new UserRegisterDto
            {
                Email = "test@mail.dk",
                FirstName = "Test",
                LastName = "Test",
                Password = "Test",
                Gender = "Test",
                Height = 1,
                Weight = 1
            };

            //uut._accountServices.IsValidEmail(register.Email).Returns(true);
            //uut._context.users.AnyAsync(x => x.Email == register.Email).Returns(true);
            var result = await uut.Register(register);

            var value = result.Result as BadRequestObjectResult;

            Assert.AreEqual(value.Value, "Email is already taken");
        }

        [Test()]
        public async Task LoginTest_ValidAsync()
        {
            //Arrange
            var request = new UserLoginDto
            {
                Email = "test@mail.dk",
                Password = "1234"
            };

            var result = await uut.Login(request);
            var value = result.Result as OkObjectResult;

            Assert.AreEqual(200, value.StatusCode);
        }

        [Test()]
        public async Task LoginTest_NotFound()
        {
            // test login with wrong email
            //Arrange
            var request = new UserLoginDto
            {
                Email = "Alan",
                Password = "test"
            };

            var result = await uut.Login(request);
            var value = result.Result as NotFoundObjectResult;

            Assert.AreEqual(404, value.StatusCode);
        }

        [Test()]
        public async Task LoginTest_UnvalidPassowrd()
        {
            // test login with wrong password
            //Arrange
            var request = new UserLoginDto
            {
                Email = "test@mail.dk",
                Password = "1235"
            };

            var result = await uut.Login(request);
            var value = result.Result as BadRequestObjectResult;

            Assert.AreEqual(400, value.StatusCode);

        }

        [Test()]
        public async Task GetuserTest_ContextUsersNull_Async()
        {
            //Arrange
            uut._context.users = null;

            var user = new User
            {
                Email = "",
                FirstName = "",
                LastName = "",
            };

            var result = await uut.GetUser(user.Email);
            var value = result.Result as NotFoundResult;

            Assert.AreEqual(404, value.StatusCode);
        }

        [Test()]
        public async Task GetUserTest_UserIsNull()
        {
            //Arrange
            var user = new User
            {
                Email = "",
                FirstName = "",
                LastName = "",
            };

            var result = await uut.GetUser(user.Email);
            var value = result.Result as NotFoundResult;
            Assert.AreEqual(404, value.StatusCode);
        }

        [Test()]
        public async Task GetUserTest_UserIsNotNull()
        {
            //Arrange
            var user = new User
            {
                Email = "test@mail.dk",
                FirstName = "Test",
                LastName = "Test",
            };

            var result = await uut.GetUser(user.Email);
            Assert.AreEqual(result.Value.Email, user.Email);

        }

        [Test()]
        public async Task PutUserTest_IdsNotEqual_AssertBadReguest()
        {
            var user = new UserDto()
            {
                Email = "1",
                FirstName = "Test",
                LastName = "Test",
                Password = "1234",
                Gender = "Test"
            };

            var result = await uut.PutUser("test", user);
            var value = result as BadRequestResult;

            Assert.AreEqual(400, value.StatusCode);
        }

        [Test()]
        public async Task PutUserTest_CangedValue_AssertSucces()
        {
            var newUser = new UserRegisterDto()
            {
                Email = "NewTest@mail.dk",
                Password = "1234",
                FirstName = "NewTest",
                LastName = "Test",
                Height = 1,
                Weight = 1
            };

            var user = new UserDto()
            {
                Email = newUser.Email,
                FirstName = "new new Test",
                LastName = newUser.LastName,
                Password = newUser.Password,
                Gender = "test"
            };
            
            await uut.Register(newUser);

            var result = uut.PutUser(newUser.Email, user);
            var value = result.Result as NoContentResult;
            
            Assert.AreEqual(204, value.StatusCode);

            await uut.DeleteUser(user.Email);
        }
        /*
        [Test()]
        public async Task PutUserTest_UserNotExsist_assertNotFound()
        {
            var newUser = new UserRegisterDto()
            {
                Email = "NewTest@mail.dk",
                Password = "1234",
                FirstName = "NewTest",
                LastName = "Test",
                Height = 1,
                Weight = 1
            };

            var user = new UserDto()
            {
                Email = newUser.Email,
                FirstName = "new new Test",
                LastName = newUser.LastName,
                Password = newUser.Password,
                Gender = "test"
            };

            //await uut.Register(newUser);

            var result = uut.PutUser(newUser.Email, user);
            var value = result.Result as NotFoundResult;

            Assert.AreEqual(404, value.StatusCode);
        }*/

        [Test()]
        public async Task DeleteUserTest_ContextNull()
        {
            uut._context.users = null;

            var user = new User
            {
                Email = "",
                FirstName = "",
                LastName = "",
            };

            var result = await uut.DeleteUser(user.Email);
            var value = result as NotFoundResult;

            Assert.AreEqual(404, value.StatusCode);
        }

        [Test()]
        public async Task DeleteUserTest_UserIsNull()
        {
            var user = new User
            {
                Email = "",
                FirstName = "",
                LastName = "",
            };

            var result = await uut.DeleteUser(user.Email);
            var value = result as NotFoundResult;

            Assert.AreEqual(404, value.StatusCode);
        }

        [Test()]
        public async Task DeleteUserTest_Success()
        {
            var user = new User
            {
                Email = "TestDelete@mail.dk",
                FirstName = "TestDelete",
                LastName = "TestDelete",
            };

            var register = new UserRegisterDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = "12345",
                Gender = "Male"
            };

            var reg_result = await uut.Register(register);
            var reg_value = reg_result.Result as AcceptedResult;

            Assert.AreEqual(202, reg_value.StatusCode);

            var del_result = uut.DeleteUser(user.Email);
            var del_value = del_result.Result as NoContentResult;

            Assert.AreEqual(204, del_value.StatusCode);
        }
        /*
        [Test()]
        public void UserExists_UserExist()
        {
            string email = "test@mail.dk";
            var result = uut.UserExists(email);

            Assert.AreEqual(true, result);
        }*/
        /*
        [Test()]
        public void UserExists_UserNotExist()
        {
            string email = "test";
            var result = uut.UserExists(email);

            Assert.AreEqual(false, result);
        }*/
    }
}