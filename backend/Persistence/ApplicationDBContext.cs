using Microsoft.EntityFrameworkCore;
using BuildingManagementSystem.Persistence.Entities;

namespace BuildingManagementSystem.Persistence;

public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
{
    public DbSet<Building> Buildings { get; set; }

}