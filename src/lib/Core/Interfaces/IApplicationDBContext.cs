using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Core.Interfaces;

public interface IApplicationDBContext
{
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
