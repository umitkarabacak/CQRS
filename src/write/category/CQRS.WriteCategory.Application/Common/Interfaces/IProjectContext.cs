using Microsoft.EntityFrameworkCore;

namespace CQRS.WriteCategory.Application.Common.Interfaces
{
    public interface IProjectContext
    {
        public DbSet<Category> Categories { get; }
        public DbSet<OutboxMessage> OutboxMessages { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
