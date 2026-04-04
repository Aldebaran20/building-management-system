using BMS.Domain.Entities;

namespace BMS.Application.DTOs.Buildings;

public record BuildingDTO(
    long Id,
    string BuildingName,
    string BuildingAddress,
    int NumberOfUnits,
    BuildingType BuildingType,
    BuildingStatus BuildingStatus,
    DateOnly DateAdded
);