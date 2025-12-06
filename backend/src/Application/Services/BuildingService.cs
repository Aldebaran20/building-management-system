using BMS.Application.DTOs;
using BMS.Application.Mappings;
using BMS.Application.Interfaces;

namespace BMS.Application.Services;

public class BuildingService : IBuildingService
{

    private readonly IBuildingRepository _repository;

    public BuildingService(IBuildingRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<BuildingDTO>> GetAllBuildingsAsync()
    {
        var buildings = await _repository.GetAllBuildingsAsync();
        return buildings.Select(b => b.MapToBuildingDto());
    }

    public async Task<BuildingDTO?> GetBuildingByIdAsync(long id)
    {     
        var building = await _repository.GetBuildingByIdAsync(id);
        return building?.MapToBuildingDto();
    }

    public async Task<BuildingDTO> CreateBuildingAsync(SaveBuildingDTO building)
    {
        var newBuilding = building
            .MapToBuildingEntity(DateOnly.FromDateTime(DateTime.UtcNow));

        var addedBuilding = await _repository.CreateBuildingAsync(newBuilding);

        return addedBuilding.MapToBuildingDto();
    }

    public async Task<bool> UpdateBuildingAsync(long id, SaveBuildingDTO building)
    {
        var existingBuilding = await _repository.GetBuildingByIdAsync(id);
        if (existingBuilding == null)
        {
            return false;
        }

        existingBuilding.UpdateFromDto(building);

        await _repository.UpdateBuildingAsync(existingBuilding);
        return true;
    }

    public async Task<bool> DeleteBuildingAsync(long id)
    {
        var existingBuilding = await _repository.GetBuildingByIdAsync(id);
        if (existingBuilding == null)
        {
            return false;
        }

        await _repository.DeleteBuildingAsync(id);
        return true;
    }

}
