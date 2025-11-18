using Microsoft.EntityFrameworkCore;
using BuildingManagementSystem.DTOs;
using BuildingManagementSystem.Persistence;
using BuildingManagementSystem.Persistence.Entities;

namespace BuildingManagementSystem.Services;

public interface IBuildingService
{
    public Task<IEnumerable<BuildingDTO>> GetAllBuildingsAsync();
    public Task<BuildingDTO?> GetBuildingByIdAsync(long id);
    public Task<BuildingDTO> CreateBuildingAsync(SaveBuildingDTO building);
    public Task<bool> UpdateBuildingAsync(long id, SaveBuildingDTO building);
    public Task<bool> DeleteBuildingAsync(long id);
}