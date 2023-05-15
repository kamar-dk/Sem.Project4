using NUnit.Framework;
using WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FA_DB.Data;
using FA_DB.Models;
using Moq;
using AutoMapper;

namespace WebApi.Controllers.Tests
{
    public class FavoriteTraningProgramsControllerTests
    {
        [SetUp]
        public void SetUp() 
        {
            var mockSet = new Mock<DbSet<FavoriteTraningPrograms>>();
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(m => m.favoriteTraningPrograms).Returns(mockSet.Object);

            var uut = new FavoriteTraningProgramsController();

        }

        [Test]
        public void GetFavoriteTraningPrograms_Test()
        {
            
        }

        [Test()]
        public void GetFavoriteTraningProgramsTest1()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void PostFavoriteTraningProgramsTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void PutFavoriteTraningProgramsTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void DeletefavoriteTraningProgramsTest()
        {
            throw new NotImplementedException();
        }
    }

    [Test()]
        public void GetFavoriteTraningProgramsTest()
        {
            Assert.Fail();
        }
    }
}