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

namespace WebApi.Controllers.Tests
{
    [TestFixture()]
    public class UsersControllerTests
    {
        DataContext mockedDbContext = new DataContext();
        UsersController uut;
        IConfiguration configuration;
        IMapper mapper;

        [SetUp] 
        public void SetUp() 
        {
            
            uut = new UsersController(mockedDbContext, mapper, configuration);
        }

        [Test()]
        public void UsersControllerTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void RegisterTest_Assert_InvalidEmail()
        {
            
            //Arrange
            var register = new UserRegisterDto
            {
                Email = "test",
                FirstName = "test",
                LastName = "test",
                Password = "test"
            };
            //Act
            var result = uut.Register(register);
            //Assert
            NUnit.Framework.Assert.AreEqual("Email is not valid", result);
            
        }

        [Test()]
        public void LoginTest()
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