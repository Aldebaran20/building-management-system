using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BMS.Infrastructure;
using BMS.Infrastructure.Repositories;
using BMS.Application.Interfaces;
using BMS.Application.Services;
using BMS.Application.Exceptions;
using BMS.Application.Validators;
using BMS.Application.Options;
using BMS.Application.DTOs.Buildings;
using BMS.Application.DTOs.Contractors;
using BMS.Application.DTOs.WorkOrders;
using BMS.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PgSQLConnection")));
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<IValidator<SaveBuildingDTO>, BuildingValidator>();
builder.Services.AddScoped<IContractorService, ContractorService>();
builder.Services.AddScoped<IContractorRepository, ContractorRepository>();
builder.Services.AddScoped<IValidator<SaveContractorDTO>, ContractorValidator>();
builder.Services.AddScoped<IWorkOrderService, WorkOrderService>();
builder.Services.AddScoped<IWorkOrderRepository, WorkOrderRepository>();
builder.Services.AddScoped<IValidator<SaveWorkOrderDTO>, WorkOrderValidator>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddAuthorization();
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.Jwt)
);
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
        ),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
    };
});
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowDevelopmentFrontend", policy =>           
        {
            policy.WithOrigins("http://localhost:5173")
                .WithMethods("GET", "POST", "PUT", "DELETE")
                .WithHeaders("Content-Type", "Authorization");
        });
    });
} else
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowProductionFrontend", policy =>
        {
            policy.WithOrigins("https://buildingmanagementsystem.xyz","https://www.buildingmanagementsystem.xyz")
                .WithMethods("GET", "POST", "PUT", "DELETE")
                .WithHeaders("Content-Type", "Authorization");
        });
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();

    if (!await context.Users.AnyAsync())
    {
        var user = new User
        {
            Email = "demo@bms.com",
            HashedPassword = BCrypt.Net.BCrypt.HashPassword("password123")
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });

    app.UseCors("AllowDevelopmentFrontend");
} else
{
    app.UseCors("AllowProductionFrontend");
}

app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }