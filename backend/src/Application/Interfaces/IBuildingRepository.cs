using BMS.Domain.Entities;

namespace BMS.Application.Interfaces;

public interface IBuildingRepository
{
    public Task<IEnumerable<Building>> GetAllBuildingsAsync();
    public Task<Building?> GetBuildingByIdAsync(long id);
    public Task<Building> CreateBuildingAsync(Building building);
    public Task UpdateBuildingAsync(Building building);
    public Task DeleteBuildingAsync(long id);
}