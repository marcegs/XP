using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence;

namespace IntegrationTests;

internal class XpAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<XpDbContext>));
            services.AddDbContext<XpDbContext>(options => { options.UseSqlServer(GetConnectionString()); });
            services.AddScoped<IXpDbContext>(provider => provider.GetService<XpDbContext>() ?? throw new InvalidOperationException());

            var dbContext = CreateDbContext(services);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        });
    }

    private string? GetConnectionString()
    {
        return "Server=localhost;Database=XP_Tests;User Id=sa;Password=Password123!@#;TrustServerCertificate=true;";
    }

    private static XpDbContext CreateDbContext(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<XpDbContext>();
        return dbContext;
    }
}