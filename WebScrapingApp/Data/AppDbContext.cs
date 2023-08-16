using Microsoft.EntityFrameworkCore;
using WebScrapingApp.Data;

namespace WebScrapingApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<SearchRecord> SearchRecords { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
