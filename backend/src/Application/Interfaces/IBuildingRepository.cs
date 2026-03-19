using BMS.Domain.Entities;

namespace BMS.Application.Interfaces;

public interface IBuildingRepository
{
    public Task<IEnumerable<Building>> GetAllBuildingsAsync(long userId);
    public Task<Building?> GetBuildingByIdAsync(long buildingId, long userId);
    public Task<Building> CreateBuildingAsync(Building building);
    public Task UpdateBuildingAsync(Building building);
    public Task DeleteBuildingAsync(Building building);
}