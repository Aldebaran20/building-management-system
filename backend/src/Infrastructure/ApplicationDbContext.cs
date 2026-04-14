using Microsoft.EntityFrameworkCore;
using BMS.Domain.Entities;

namespace BMS.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Contractor> Contractors { get; set; }
    public DbSet<WorkOrder> WorkOrders { get; set; }
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}