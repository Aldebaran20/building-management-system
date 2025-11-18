using BuildingManagementSystem.Persistence.Entities;
using BuildingManagementSystem.DTOs;

namespace BuildingManagementSystem.Mappings;

public static class BuildingMappingsExtensions
{
    public static BuildingDTO MapToBuildingDto(this Building building)
    {
        return new BuildingDTO(
            building.Id,
            building.BuildingName,
            building.BuildingAddress,
            building.NumberOfUnits,
            building.BuildingType,
            building.BuildingStatus,
            building.DateAdded
        );
    }

    public static Building MapToBuildingEntity(this BuildingDTO buildingDto)
    {
        return new Building
        {
            Id = buildingDto.Id,
            BuildingName = buildingDto.BuildingName,
            BuildingAddress = buildingDto.BuildingAddress,
            NumberOfUnits = buildingDto.NumberOfUnits,
            BuildingType = buildingDto.BuildingType,
            BuildingStatus = buildingDto.BuildingStatus,
            DateAdded = buildingDto.DateAdded
        };
    }
}