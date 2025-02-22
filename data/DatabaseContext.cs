using Microsoft.EntityFrameworkCore;

namespace erp_back.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Authentication> Authentications { get; set; }


    }
}