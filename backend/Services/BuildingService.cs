using Microsoft.EntityFrameworkCore;
using BuildingManagementSystem.DTOs.Buildings;
using BuildingManagementSystem.Data;
using BuildingManagementSystem.Data.Entities;

namespace BuildingManagementSystem.Services;

public interface IBuildingService
{
    public Task<IEnumerable<BuildingDTO>> GetAllBuildingsAsync();
    public Task<BuildingDTO?> GetBuildingByIdAsync(long id);
    public Task<Building> CreateBuildingAsync(SaveBuildingDTO building);
    public Task UpdateBuildingAsync(long id, SaveBuildingDTO building);
    public Task DeleteBuildingAsync(long id);
    public Task<bool> BuildingExists(long id);
}

public class BuildingService : IBuildingService
{

    private readonly ApplicationDBContext _context;

    public BuildingService(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BuildingDTO>> GetAllBuildingsAsync()
    {
        return await _context.Buildings
            .Select(b => new BuildingDTO(
                b.Id,
                b.BuildingName,
                b.BuildingAddress,
                b.NumberOfUnits,
                b.BuildingType,
                b.BuildingStatus,
                b.DateAdded
            ))
            .ToListAsync();
    }

    public async Task<BuildingDTO?> GetBuildingByIdAsync(long id)
    {     
        return await _context.Buildings
            .Where(b => b.Id == id)
            .Select(b => new BuildingDTO(
                b.Id,
                b.BuildingName,
                b.BuildingAddress,
                b.NumberOfUnits,
                b.BuildingType,
                b.BuildingStatus,
                b.DateAdded
            ))
            .FirstOrDefaultAsync();
    }

    public async Task<Building> CreateBuildingAsync(SaveBuildingDTO building)
    {
        var newBuilding = new Building
        {
            BuildingName = building.BuildingName,
            BuildingAddress = building.BuildingAddress,
            NumberOfUnits = building.NumberOfUnits,
            BuildingType = building.BuildingType,
            BuildingStatus = building.BuildingStatus,
            DateAdded = DateOnly.FromDateTime(DateTime.UtcNow)
        };
        _context.Buildings.Add(newBuilding);
        await _context.SaveChangesAsync();
        return newBuilding;
    }

    public async Task UpdateBuildingAsync(long id, SaveBuildingDTO building)
    {
        var existingBuilding = await _context.Buildings.FindAsync(id);
        if (existingBuilding == null)
        {
            return;
        }

        existingBuilding.BuildingName = building.BuildingName;
        existingBuilding.BuildingAddress = building.BuildingAddress;
        existingBuilding.NumberOfUnits = building.NumberOfUnits;
        existingBuilding.BuildingType = building.BuildingType;
        existingBuilding.BuildingStatus = building.BuildingStatus;

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
