using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using BMS.Infrastructure;

namespace BMS.IntegrationTests;

public sealed class CustomWebApplicationFactory(string connectionString) 
:   WebApplicationFactory<Program>
{
    private readonly string _connectionString = connectionString;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        { 
            services.Remove(
                services.SingleOrDefault(
                    service => 
                        typeof(DbContextOptions<ApplicationDbContext>) == service.ServiceType
                )!
            );
            services.AddDbContext<ApplicationDbContext>(
                (_, option) => option.UseNpgsql(_connectionString)
            );
        });
    }
}