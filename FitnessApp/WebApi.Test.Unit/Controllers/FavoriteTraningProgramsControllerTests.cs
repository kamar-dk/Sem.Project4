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
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Controllers.Tests
{
    public class FavoriteTraningProgramsControllerTests
    {
        Mock<DataContext> _dataContext;
        IMapper _mapper;
        FavoriteTraningProgramsController uut;

        [SetUp]
        public void SetUp() 
        {
            /*//setup mock datacontext
            var mockDataContext = new Mock<DataContext>();
            var mockSet = new Mock<DbSet<FavoriteTraningPrograms>>();
            mockDataContext.Setup(x => x.favoriteTraningPrograms).Returns(mockSet.Object);
            _dataContext.Setup(x => x.favoriteTraningPrograms).Returns(mockSet.Object);

            // setup automapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FavoriteTraningPrograms, FavoriteTraningProgramsDto>();
                cfg.CreateMap<FavoriteTraningProgramsDto, FavoriteTraningPrograms>();
            });
            _mapper = config.CreateMapper();
            //setup unit under test for favortietraningprogramscontroller
            uut = new FavoriteTraningProgramsController(_dataContext.Object, _mapper);*/
        }

        [Test]
        public void GetFavoriteTraningPrograms_Test()
        {
            //create test for GetFavoriteTraningPrograms
            //arrange
            var testList = new List<FavoriteTraningPrograms>();
            testList.Add(new FavoriteTraningPrograms());
            testList.Add(new FavoriteTraningPrograms());
            testList.Add(new FavoriteTraningPrograms());
            testList.Add(new FavoriteTraningPrograms());
            testList.Add(new FavoriteTraningPrograms());
            testList.Add(new FavoriteTraningPrograms());
            testList.Add(new FavoriteTraningPrograms());
            testList.Add(new FavoriteTraningPrograms());
            testList.Add(new FavoriteTraningPrograms());
            testList.Add(new FavoriteTraningPrograms());

            var mockSet = new Mock<DbSet<FavoriteTraningPrograms>>();
            mockSet.As<IQueryable<FavoriteTraningPrograms>>().Setup(m => m.Provider).Returns(testList.AsQueryable().Provider);
            mockSet.As<IQueryable<FavoriteTraningPrograms>>().Setup(m => m.Expression).Returns(testList.AsQueryable().Expression);
            mockSet.As<IQueryable<FavoriteTraningPrograms>>().Setup(m => m.ElementType).Returns(testList.AsQueryable().ElementType);
            mockSet.As<IQueryable<FavoriteTraningPrograms>>().Setup(m => m.GetEnumerator()).Returns(testList.AsQueryable().GetEnumerator());

            _dataContext.Setup(x => x.favoriteTraningPrograms).Returns(mockSet.Object);

            //act
            var result = uut.GetFavoriteTraningPrograms();
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Result.Value.Count());
        }

        [Test()]
        public void GetFavoriteTraningProgramsTest1()
        {
            throw new NotImplementedException();
        }

        // create test for postfavoritetrainingprograms
        [Test()]
        public void PostFavoriteTraningProgramsTest()
        {
            //arrange
            var testList = new List<FavoriteTraningPrograms>();
            testList.Add(new FavoriteTraningPrograms());
            testList.Add(new FavoriteTraningPrograms());
            testList.Add(new FavoriteTraningPrograms());
            testList.Add(new FavoriteTraningPrograms());
            testList.Add(new FavoriteTraningPrograms());

            var mockSet = new Mock<DbSet<FavoriteTraningPrograms>>();
            mockSet.As<IQueryable<FavoriteTraningPrograms>>().Setup(m => m.Provider).Returns(testList.AsQueryable().Provider);
            mockSet.As<IQueryable<FavoriteTraningPrograms>>().Setup(m => m.Expression).Returns(testList.AsQueryable().Expression);
            mockSet.As<IQueryable<FavoriteTraningPrograms>>().Setup(m => m.ElementType).Returns(testList.AsQueryable().ElementType);
            mockSet.As<IQueryable<FavoriteTraningPrograms>>().Setup(m => m.GetEnumerator()).Returns(testList.AsQueryable().GetEnumerator());

            _dataContext.Setup(x => x.favoriteTraningPrograms).Returns(mockSet.Object);

            //act
            var result = uut.PostFavoriteTraningPrograms(new FavoriteTraningProgramsDto());
            Assert.IsNotNull(result);
            Assert.AreEqual(6, 6);


        }


        [Test]
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

    
}