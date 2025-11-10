using Microsoft.EntityFrameworkCore;

namespace BuildingManagementSystem.Models;

public class BuildingContext : DbContext
{
    public BuildingContext(DbContextOptions<BuildingContext> options)
        : base(options)
    {
    }

    public DbSet<Building> Buildings { get; set; } = null!;
}