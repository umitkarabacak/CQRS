using CQRS.Database.Creator.Domain;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Database.Creator.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
    }
}
