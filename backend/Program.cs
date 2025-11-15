using Microsoft.EntityFrameworkCore;
using BuildingManagementSystem.Data;
using BuildingManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDBContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PgSQLConnection")));
    // opt.UseInMemoryDatabase("BuildingList"));
builder.Services.AddScoped<IBuildingService, BuildingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDBContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
