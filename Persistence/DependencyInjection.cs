using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<XpDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("XP")));
        services.AddScoped<IXpDbContext>(provider => provider.GetService<XpDbContext>() ?? throw new InvalidOperationException());
        
        return services;
    }
}