using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<ApplicationBuilder> Opts):base(Opts) {}

    }
}
