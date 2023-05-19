using Microsoft.EntityFrameworkCore;

namespace easyNetAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }


    }
}