using Microsoft.EntityFrameworkCore;
using BuildingManagementSystem.DTOs;
using BuildingManagementSystem.Persistence;
using BuildingManagementSystem.Persistence.Entities;
using BuildingManagementSystem.Mappings;

namespace BuildingManagementSystem.Services;

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
            .Select(b => b.MapToBuildingDto())
            .ToListAsync();
    }

    public async Task<BuildingDTO?> GetBuildingByIdAsync(long id)
    {     
        return await _context.Buildings
            .Where(b => b.Id == id)
            .Select(b => b.MapToBuildingDto())
            .FirstOrDefaultAsync();
    }

    public async Task<BuildingDTO> CreateBuildingAsync(SaveBuildingDTO building)
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

        return newBuilding.MapToBuildingDto();
    }

    public async Task<bool> UpdateBuildingAsync(long id, SaveBuildingDTO building)
    {
        var existingBuilding = await _context.Buildings.FindAsync(id);
        if (existingBuilding == null)
        {
            return false;
        }

        existingBuilding.BuildingName = building.BuildingName;
        existingBuilding.BuildingAddress = building.BuildingAddress;
        existingBuilding.NumberOfUnits = building.NumberOfUnits;
        existingBuilding.BuildingType = building.BuildingType;
        existingBuilding.BuildingStatus = building.BuildingStatus;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteBuildingAsync(long id)
    {
        var building = await _context.Buildings.FindAsync(id);
        if (building == null)
        {
            return false;
        }

        _context.Buildings.Remove(building);
        await _context.SaveChangesAsync();

        return true;
    }

}
