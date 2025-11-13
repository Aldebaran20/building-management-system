using Microsoft.EntityFrameworkCore;

namespace BuildingManagementSystem.Models;

public class BuildingContext : DbContext
{
    public DbSet<Building> Buildings { get; set; }

    public BuildingContext(DbContextOptions<BuildingContext> options)
        : base(options)
    {
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Building>().ToTable("Building");
    // }
}