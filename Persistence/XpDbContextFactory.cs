using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class XpDbContextFactory : DesignTimeDbContextFactoryBase<XpDbContext>
{
    protected override XpDbContext CreateNewInstance(DbContextOptions<XpDbContext> options)
    {
        return new XpDbContext(options);
    }
}