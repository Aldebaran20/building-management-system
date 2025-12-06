using Testcontainers.PostgreSql;

public class PostgresFixture : IAsyncLifetime
{
    public readonly PostgreSqlContainer Postgres = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .Build();

    public Task InitializeAsync()
    {
        return Postgres.StartAsync();
    }

    public Task DisposeAsync()
    {
        return Postgres.DisposeAsync().AsTask();
    }
}