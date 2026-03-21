using Microsoft.AspNetCore.Mvc.Testing;
using BMS.Infrastructure;
using System.Text.Json;
using BMS.Domain.Entities;

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

    protected async Task AuthenticateAsync()
    {
        var loginResponse = await _httpClient.PostAsJsonAsync("/api/auth/login", new
        {
            Email = "admin@bms.com",
            Password = "password123"
        });

        var content = await loginResponse.Content.ReadFromJsonAsync<JsonElement>();
        var token = content.GetProperty("token").GetString();

        _httpClient.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }

    protected async Task ResetDatabaseAsync()
    {
        using var scope = _webApplicationFactory.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        
        context.Buildings.RemoveRange(context.Buildings);
        context.Users.RemoveRange(context.Users);

        context.Users.Add(new User
        {
            Email = "admin@bms.com",
            HashedPassword = BCrypt.Net.BCrypt.HashPassword("password123")
        });

        await context.SaveChangesAsync();
    }
}