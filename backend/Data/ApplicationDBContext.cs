using Microsoft.EntityFrameworkCore;
using BuildingManagementSystem.Data.Entities;

namespace BuildingManagementSystem.Data;

public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
{
    public DbSet<Building> Buildings { get; set; }

}