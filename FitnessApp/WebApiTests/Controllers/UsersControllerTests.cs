using NUnit.Framework;
using WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCore.Testing.NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Data;
using WebApi.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.Services;
using WebApi.DTO;
using NSubstitute;
using Moq;
using NSubstitute.ReceivedExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using NuGet.Common;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.InMemory;
using Azure.Core;

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
            //uut._accountServices.IsVaildEmail(register.Email).Returns(true);
            //uut._context.users.AnyAsync(x => x.Email == register.Email).Returns(true);
            var result = await uut.Register(register);
            var value = result.Result as BadRequestObjectResult;
            
            NUnit.Framework.Assert.AreEqual(value.Value, "Email is not valid");
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
                Password = "Test"
            };

            //uut._accountServices.IsVaildEmail(register.Email).Returns(true);
            //uut._context.users.AnyAsync(x => x.Email == register.Email).Returns(true);
            var result = await uut.Register(register);

            var value = result.Result as BadRequestObjectResult;

            NUnit.Framework.Assert.AreEqual(value.Value, "Email is already taken");
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

            NUnit.Framework.Assert.AreEqual(200, value.StatusCode);   
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

            NUnit.Framework.Assert.AreEqual(404, value.StatusCode);     
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

            NUnit.Framework.Assert.AreEqual(400, value.StatusCode);

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

            NUnit.Framework.Assert.AreEqual(404, value.StatusCode);
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
            NUnit.Framework.Assert.AreEqual(404, value.StatusCode);
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
            NUnit.Framework.Assert.AreEqual(result.Value.Email, user.Email);

        }

        [Test()]
        public void PutUserTest()
        {

            throw new NotImplementedException();
        }

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

            NUnit.Framework.Assert.AreEqual(404, value.StatusCode);
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

            NUnit.Framework.Assert.AreEqual(404, value.StatusCode);
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

            NUnit.Framework.Assert.AreEqual(202, reg_value.StatusCode);

            var del_result = uut.DeleteUser(user.Email);
            var del_value = del_result.Result as NoContentResult;

            NUnit.Framework.Assert.AreEqual(204, del_value.StatusCode);
        }

        [Test()]
        public void UserExists_UserExist()
        {
            string email = "test@mail.dk";
            var result = uut.UserExists(email);

            NUnit.Framework.Assert.AreEqual(true, result);
        }

        [Test()]
        public void UserExists_UserNotExist()
        {
            string email = "test";
            var result = uut.UserExists(email);

            NUnit.Framework.Assert.AreEqual(false, result);
        }
    }
}