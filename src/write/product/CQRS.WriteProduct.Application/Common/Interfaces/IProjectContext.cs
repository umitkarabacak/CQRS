using CQRS.WriteProduct.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS.WriteProduct.Application.Common.Interfaces
{
    public interface IProjectContext
    {
        public DbSet<Product> Products { get; }
        public DbSet<OutboxMessage> OutboxMessages { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
