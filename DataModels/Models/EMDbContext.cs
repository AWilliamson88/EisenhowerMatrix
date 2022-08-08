using Microsoft.EntityFrameworkCore;

namespace DataModels.Models
{
    public class EMDbContext : DbContext
    {
        public EMDbContext(DbContextOptions<EMDbContext> options) : base(options)
        {

        }

        public DbSet<EMList> EMLists { get; set; }
        public DbSet<EMListItem> EMListItems { get; set; }

    }
}
