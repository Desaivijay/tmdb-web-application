using Microsoft.EntityFrameworkCore;

namespace tmdb_web_application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define your DbSet properties for your models here
        // For example:
        // public DbSet<Movie> Movies { get; set; }
    }
}
