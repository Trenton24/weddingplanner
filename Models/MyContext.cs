using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;
namespace WeddingPlanner.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<Users> users {get;set;}

        public DbSet<WeddingPlanner.Models.Wedding> Wedding { get; set; }
    }
}
