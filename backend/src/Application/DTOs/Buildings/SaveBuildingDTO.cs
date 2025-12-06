using BMS.Domain.Entities;

namespace BMS.Application.DTOs;

public record SaveBuildingDTO(
    string BuildingName,
    string BuildingAddress,
    int NumberOfUnits,
    BuildingType BuildingType,
    BuildingStatus BuildingStatus
);