using Microsoft.EntityFrameworkCore;

namespace CQRS.Distributor.App.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }
    }
}
