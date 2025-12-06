using BMS.Domain.Entities;
using BMS.Application.DTOs;

namespace BMS.Application.Mappings;

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

    public static Building MapToBuildingEntity(this SaveBuildingDTO dto, DateOnly dateAdded)
    {
        return new Building
        {
            BuildingName = dto.BuildingName,
            BuildingAddress = dto.BuildingAddress,
            NumberOfUnits = dto.NumberOfUnits,
            BuildingType = dto.BuildingType,
            BuildingStatus = dto.BuildingStatus,
            DateAdded = dateAdded
        };
    }

    public static void UpdateFromDto(this Building building, SaveBuildingDTO dto)
    {
        building.BuildingName = dto.BuildingName;
        building.BuildingAddress = dto.BuildingAddress;
        building.NumberOfUnits = dto.NumberOfUnits;
        building.BuildingType = dto.BuildingType;
        building.BuildingStatus = dto.BuildingStatus;
    }
}