using CQRS.Write.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Write.Application.Common.Interfaces
{
    public interface IProjectContext
    {
        public DbSet<Category> Categories { get; }
        public DbSet<Product> Products { get; }
        public DbSet<OutboxMessage> OutboxMessages { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
