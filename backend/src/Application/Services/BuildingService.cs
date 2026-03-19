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

    public async Task<IEnumerable<BuildingDTO>> GetAllBuildingsAsync(long userId)
    {
        var buildings = await _repository.GetAllBuildingsAsync(userId);
        return buildings.Select(b => b.MapToBuildingDto());
    }

    public async Task<BuildingDTO?> GetBuildingByIdAsync(long buildingId, long userId)
    {     
        var building = await _repository.GetBuildingByIdAsync(buildingId, userId);
        return building?.MapToBuildingDto();
    }

    public async Task<BuildingDTO> CreateBuildingAsync(SaveBuildingDTO buildingDto, long userId)
    {
        var newBuilding = buildingDto.MapToBuildingEntity(userId);

        var addedBuilding = await _repository.CreateBuildingAsync(newBuilding);

        return addedBuilding.MapToBuildingDto();
    }

    public async Task<bool> UpdateBuildingAsync(long buildingId, SaveBuildingDTO buildingDto, long userId)
    {
        var existingBuilding = await _repository.GetBuildingByIdAsync(buildingId, userId);
        if (existingBuilding == null)
        {
            return false;
        }

        existingBuilding.BuildingName = buildingDto.BuildingName;
        existingBuilding.BuildingAddress = buildingDto.BuildingAddress;
        existingBuilding.NumberOfUnits = buildingDto.NumberOfUnits;
        existingBuilding.BuildingType = buildingDto.BuildingType;
        existingBuilding.BuildingStatus = buildingDto.BuildingStatus;

        await _repository.UpdateBuildingAsync(existingBuilding);
        return true;
    }

    public async Task<bool> DeleteBuildingAsync(long buildingId, long userId)
    {
        var existingBuilding = await _repository.GetBuildingByIdAsync(buildingId, userId);
        if (existingBuilding == null)
        {
            return false;
        }

        await _repository.DeleteBuildingAsync(existingBuilding);
        return true;
    }

}
