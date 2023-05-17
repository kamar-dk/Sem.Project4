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

namespace WebApi.Controllers.Tests
{
    [TestFixture()]
    public class UsersControllerTests
    {
        DataContext mockedDbContext = new DataContext();
        
        public IUserServices _userServices;
        public DataContext _context;
        public UsersController uut;

        [SetUp]
        public void SetUp()
        {
            _userServices = Substitute.For<IUserServices>();
            _context = Substitute.For<DataContext>();
            uut = new UsersController(mockedDbContext, _userServices);
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
        public void RegisterTest_Assert_EmailAlreadyTaken(string Expected)
        {
            
            //Arrange
            var register = new UserRegisterDto
            {
                Email = "test",
                FirstName = "Test",
                LastName = "Test",
                Password = "Test"
            };

            uut._accountServices.IsVaildEmail(register.Email).Returns(true);

            uut._context.users.AnyAsync(x => x.Email == register.Email).Returns(true);

            uut.Register(register);
            NUnit.Framework.Assert.AreEqual("Email is already taken", Expected);

        }

        [Test()]
        public void LoginTest_assert_ok()
        {

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