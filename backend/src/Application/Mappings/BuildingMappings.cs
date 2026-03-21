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

    public static Building MapToBuildingEntity(this SaveBuildingDTO dto, long userId)
    {
        return new Building
        {
            UserId = userId,
            BuildingName = dto.BuildingName,
            BuildingAddress = dto.BuildingAddress,
            NumberOfUnits = dto.NumberOfUnits,
            BuildingType = dto.BuildingType,
            BuildingStatus = dto.BuildingStatus,
            DateAdded = DateOnly.FromDateTime(DateTime.UtcNow)
        };
    }

}