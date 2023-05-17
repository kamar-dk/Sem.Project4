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
        IConfiguration _configuration = Substitute.For<IConfiguration>();
        IServiceCollection _services;
        private IServiceProvider? serviceProvider;

        
        
        [SetUp]
        public void SetUp()
        {

            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(@"Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=kaspermartensen_Prj4;User ID=kaspermartensen_Prj4;Password=Bed2Fed2;Encrypt=False; Trust Server Certificate=False;Persist Security Info = True;")
                .Options;

            mockedDbContext = new DataContext(contextOptions);
            _context = mockedDbContext;
            _userServices = Substitute.For<IUserServices>();
            _mapper = Substitute.For<IMapper>();
            _configuration = Substitute.For<IConfiguration>();
            _services = Substitute.For<IServiceCollection>();
            serviceProvider = Substitute.For<IServiceProvider>();
            _services.AddSingleton(serviceProvider);
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

        [Test]
        public async Task LoginTest_ValidAsync()
        {
            var token = "200OK";
            //Arrange
            var request = new UserLoginDto
            {
                Email = "test@mail.dk",
                Password = "1234"
            };
            //uut._context.users.AnyAsync(x => x.Email == request.Email).Returns(true);
            //uut._accountServices.VerifyPasswordHash(request.Password, request.Password, request.Password).Returns(true);

            
            var result = await uut.Login(request);
            
            
            var value = result.Result as OkObjectResult;

            NUnit.Framework.Assert.AreEqual( token, value);
            
        }

        [Test()]
        public void LoginTest_NotFound()
        {
            // test login with wrong email
            //Arrange
            var request = new UserLoginDto
            { 
                Email = "Alan@mail.dk",
                Password = "test"
            };

            //uut._context.users.AnyAsync(x => x.Email == request.Email).Returns(false)
            var result = uut.Login(request);

            NUnit.Framework.Assert.IsInstanceOf<NotFoundResult>(result.Result);

            
        }

        [Test()]
        public async Task GetuserTestAsync()
        {
            //Arrange
            User user = new User
            {
                Email = "test",
                FirstName = "Test",
                LastName = "Test"
            };
            /*_context.users.Add(user);
            _context.SaveChanges();*/

        var result = await uut.GetUser(user.Email);
            NUnit.Framework.Assert.AreEqual(result.Value.Email, user.Email);


        }

        [Test()]
        public void PutUserTest()
        {


            throw new NotImplementedException();
        }

        [Test()]
        public void DeleteUserTest()
        {
            throw new NotImplementedException();
        }
    }
}