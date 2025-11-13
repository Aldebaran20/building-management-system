using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildingManagementSystem.Models;

namespace BuildingManagementSystem.Services;

public interface IBuildingService
{
    public Task<IEnumerable<Building>> GetAllBuildingsAsync();
    public Task<Building?> GetBuildingByIdAsync(long id);
    public Task<Building> CreateBuildingAsync(Building building);
    public Task UpdateBuildingAsync(long id, Building building);
    public Task DeleteBuildingAsync(long id);
    public Task<bool> BuildingExists(long id);
}

public class BuildingService : IBuildingService
{

    private readonly BuildingContext _context;

    public BuildingService(BuildingContext context)
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

    public async Task UpdateBuildingAsync(long id, Building building)
    {
        if (id != building.Id)
        {
            return;
        }

        _context.Entry(building).State = EntityState.Modified;

        await _context.SaveChangesAsync();
        return;
    }

    public async Task DeleteBuildingAsync(long id)
    {
        var building = await _context.Buildings.FindAsync(id);
        if (building == null)
        {
            return;
        }

        _context.Buildings.Remove(building);
        await _context.SaveChangesAsync();

        return;
    }

    public async Task<bool> BuildingExists(long id)
    {
        return await _context.Buildings.AnyAsync(e => e.Id == id);
    }

}
