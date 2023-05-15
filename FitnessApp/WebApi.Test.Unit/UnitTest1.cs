using FA_DB.Data;
using FA_DB.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;


namespace WebApi.Test.Unit
{
    public class Tests
    {
        
      
        public void setup()
        {
            var mockSet = new Mock<DbSet<FavoriteTraningPrograms>>();
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(m => m.favoriteTraningPrograms).Returns(mockSet.Object);
        }

        [Test]
        public 
    }
}