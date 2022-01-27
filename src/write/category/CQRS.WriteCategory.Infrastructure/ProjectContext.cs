namespace CQRS.WriteCategory.Infrastructure
{
    public class ProjectContext : DbContext, IProjectContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
    }
}
