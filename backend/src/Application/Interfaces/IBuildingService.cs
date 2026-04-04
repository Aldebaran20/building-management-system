using BMS.Application.DTOs.Buildings;

namespace BMS.Application.Interfaces;

public interface IBuildingService
{
    public Task<IEnumerable<BuildingDTO>> GetAllBuildingsAsync(long userId);
    public Task<BuildingDTO?> GetBuildingByIdAsync(long buildingId, long userId);
    public Task<BuildingDTO> CreateBuildingAsync(SaveBuildingDTO buildingDto, long userId);
    public Task<bool> UpdateBuildingAsync(long buildingId, SaveBuildingDTO buildingDto, long userId);
    public Task<bool> DeleteBuildingAsync(long buildingId, long userId);
}