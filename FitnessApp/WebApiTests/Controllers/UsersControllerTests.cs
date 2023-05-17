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

namespace WebApi.Controllers.Tests
{
    [TestFixture()]
    public class UsersControllerTests
    {
        DataContext mockedDbContext;
        
        public IUserServices _userServices;
        public DataContext _context;
        public UsersController uut;
        public IMapper _mapper;
        IConfiguration _configuration;

        [SetUp]
        public void SetUp()
        {
            
            // make Datacontext and use the Database connectionstring in appsettings don't use inmemory database

            


            //mockedDbContext = new DataContext(new DbContextOptionsBuilder<DataContext>().UseSqlite("Filename=:memory:").Options);
            _userServices = Substitute.For<IUserServices>();
            _context = Substitute.For<DataContext>();
            uut = new UsersController(mockedDbContext, _mapper, _configuration);



            /*
            _userServices = Substitute.For<IUserServices>();
            _context = Substitute.For<DataContext>();
            uut = new UsersController(mockedDbContext, _userServices);*/

        }

        [TestCase("Email is not valid")]
        public async Task RegisterTest_Assert_InvalidEmailAsync(string Expected)
        {
            //Arrange
            
            var register = new UserRegisterDto
            { 
                Email = "test",
                FirstName = "Test",
                LastName = "Test",
                Password = "Test"
            };

            uut._accountServices.IsVaildEmail(register.Email).Returns(false);
            await uut.Register(register);
            NUnit.Framework.Assert.AreEqual("Email is not valid", Expected);
        }

        [TestCase("Email is already taken")]
        public async Task RegisterTest_Assert_EmailAlreadyTakenAsync(string Expected)
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
            

            uut._context.users.AnyAsync(x => x.Email == register.Email).Returns(true);

            await uut.Register(register);
            NUnit.Framework.Assert.AreEqual("Email is already taken", Expected);

        }

        [Test]
        public void LoginTest_Valid()
        {
            var token = "200OK";
            //Arrange
            var request = new UserLoginDto
            {
                Email = "Jeppe@mail.dk",
                Password = "1234"
            };
            //uut._context.users.AnyAsync(x => x.Email == request.Email).Returns(true);
            //uut._accountServices.VerifyPasswordHash(request.Password, request.Password, request.Password).Returns(true);
            var result = uut.Login(request);

            NUnit.Framework.Assert.AreEqual(result, token);
            
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
        public void GetusersTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void GetUserTest()
        {
            throw new NotImplementedException();
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