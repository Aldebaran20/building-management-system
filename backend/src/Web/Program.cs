using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BMS.Infrastructure;
using BMS.Infrastructure.Repositories;
using BMS.Application.Interfaces;
using BMS.Application.Services;
using BMS.Application.Exceptions;
using BMS.Application.Validators;
using BMS.Application.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PgSQLConnection")));
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<IValidator<SaveBuildingDTO>, BuildingValidator>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowDevelopmentFrontend", policy =>           
    {
        policy.WithOrigins("http://localhost:5173")
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .WithHeaders("Content-Type");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
    }
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseCors("AllowDevelopmentFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }