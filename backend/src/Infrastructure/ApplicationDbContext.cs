using Microsoft.EntityFrameworkCore;
using BMS.Domain.Entities;

namespace BMS.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Building> Buildings { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}