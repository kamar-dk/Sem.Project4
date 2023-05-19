using NUnit.Framework;
using WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using WebApi.Data;
using WebApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.DTO;

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
            uut = new FavoriteTraningProgramsController(_context, _mapper);
        }

        [Test()]
        public void FavoriteTraningProgramsControllerTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void GetFavoriteTraningProgramsTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void GetFavoriteTraningProgramsTest1()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void PostFavoriteTraningProgramsTest()
        {
            var ftpDto = new FavoriteTraningProgramsDto()
            {
                FavoriteTraningProgramsID = 1,
                TraningProgramID = 1,
                Email = "test@mail.dk"
            };
        }

        [Test()]
        public void PutFavoriteTraningProgramsTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void DeletefavoriteTraningProgramsTest()
        {
            
        }

        [Test()]
        public void DeletefavoriteTraningProgramsTest_Invaid()
        {

            
            
        }
    }
}