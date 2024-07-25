using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IXpDbContext
{
    DbSet<User> Users { get; }
    DbSet<Account> Accounts { get; }
    DbSet<Trade> Trades { get; }
    DbSet<Product> Products { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken);
}