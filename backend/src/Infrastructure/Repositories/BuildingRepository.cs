using Microsoft.EntityFrameworkCore;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;


namespace BMS.Infrastructure.Repositories;

public class BuildingRepository : IBuildingRepository
{

    private readonly ApplicationDbContext _context;

    public BuildingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Building>> GetAllBuildingsAsync()
    {
        return await _context.Buildings.ToListAsync();
    }

    public async Task<Building?> GetBuildingByIdAsync(long id)
    {
        return await _context.Buildings.FindAsync(id);
    }

    public async Task<Building> CreateBuildingAsync(Building building)
    {
        _context.Buildings.Add(building);
        await _context.SaveChangesAsync();
        return building;
    }

    public async Task UpdateBuildingAsync(Building building)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBuildingAsync(long id)
    {
        var building = await _context.Buildings.FindAsync(id);
        if (building == null) return;
        _context.Buildings.Remove(building);
        await _context.SaveChangesAsync();
    }
}