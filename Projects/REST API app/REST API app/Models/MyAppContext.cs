using Microsoft.EntityFrameworkCore;

namespace REST_API_app.Models
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> opt) 
            : base(opt)
        {

        }

        public DbSet<Command> Commands { get; set; }
    }
}
