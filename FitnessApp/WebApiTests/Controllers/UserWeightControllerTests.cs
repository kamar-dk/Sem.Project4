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

namespace WebApi.Controllers.Tests
{
    [TestFixture()]
    public class UserWeightControllerTests
    {

        DataContext mockedDbContext;

        [SetUp] 
        public void SetUp() 
        {
            mockedDbContext = Create.MockedDbContextFor<DataContext>();


            

        }

        [Test()]
        public void DeleteUserWeightTest()
        {
            throw new NotImplementedException();
        }
    }
}