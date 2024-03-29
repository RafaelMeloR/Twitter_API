using Microsoft.EntityFrameworkCore;

namespace TwitterAPI.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Tweet> Tweet { get; set; }
    }
 
}
