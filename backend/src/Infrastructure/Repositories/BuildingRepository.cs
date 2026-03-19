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

    public async Task<IEnumerable<Building>> GetAllBuildingsAsync(long userId)
    {
        return await _context.Buildings
            .Where(b => b.UserId == userId).ToListAsync();
    }

    public async Task<Building?> GetBuildingByIdAsync(long buildingId, long userId)
    {
        return await _context.Buildings
            .FirstOrDefaultAsync(b => b.Id == buildingId && b.UserId == userId);
    }

    public async Task<Building> CreateBuildingAsync(Building building)
    {
        _context.Buildings.Add(building);
        await _context.SaveChangesAsync();
        return building;
    }

    public async Task UpdateBuildingAsync(Building building)
    {
        _context.Entry(building).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBuildingAsync(Building building)
    {
        _context.Buildings.Remove(building);
        await _context.SaveChangesAsync();
    }
}