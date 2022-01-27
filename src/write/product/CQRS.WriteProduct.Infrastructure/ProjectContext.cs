namespace CQRS.WriteProduct.Infrastructure
{
    public class ProjectContext : DbContext, IProjectContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
    }
}
