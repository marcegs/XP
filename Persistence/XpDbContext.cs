using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence;

public class XpDbContext : DbContext, IXpDbContext
{
    public XpDbContext([NotNull] DbContextOptions<XpDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Trade> Trades { get; set; }
    public DbSet<Product> Products { get; set; }

    public new async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: AuditableEntity, State: EntityState.Added or EntityState.Modified });

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
                ((AuditableEntity)entityEntry.Entity).CreationDate = DateTime.Now;
            
            ((AuditableEntity)entityEntry.Entity).LastUpdate = DateTime.Now;
        }
        
        await base.SaveChangesAsync(cancellationToken);
    }
}