using Microsoft.EntityFrameworkCore;
namespace WeddingPlanner.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<Users> users {get;set;}
    }
}
