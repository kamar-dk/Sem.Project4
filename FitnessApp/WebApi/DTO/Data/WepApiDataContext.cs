using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace WebApi.DTO.Data
{
    public class WepApiDataContext : IdentityDbContext
    {
        public WepApiDataContext(DbContextOptions<WepApiDataContext> options) : base(options)
        {
        }
    }
}
