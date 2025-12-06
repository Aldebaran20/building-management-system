using Microsoft.AspNetCore.Mvc.Testing;
using BMS.Infrastructure;

namespace BMS.IntegrationTests;

public abstract class IntegrationTestBase 
:   IClassFixture<PostgresFixture>,
    IDisposable
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    protected readonly HttpClient _httpClient;

    public IntegrationTestBase(PostgresFixture fixture)
    {
        var clientOptions = new WebApplicationFactoryClientOptions();
        clientOptions.AllowAutoRedirect = false;
        var connString = fixture.Postgres.GetConnectionString();
        
        _webApplicationFactory = new CustomWebApplicationFactory(connString);
        _httpClient = _webApplicationFactory.CreateClient(clientOptions);
    }

    public void Dispose()
    {
        _webApplicationFactory.Dispose();
        GC.SuppressFinalize(this);
    }

    protected async Task ResetDatabaseAsync()
    {
        using var scope = _webApplicationFactory.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Buildings.RemoveRange(context.Buildings);
        await context.SaveChangesAsync();
    }
}