using BuildingManagementSystem.Data.Entities;

namespace BuildingManagementSystem.DTOs.Buildings;

public record BuildingDTO(
    long Id,
    string BuildingName,
    string BuildingAddress,
    int NumberOfUnits,
    BuildingType BuildingType,
    BuildingStatus BuildingStatus,
    DateOnly DateAdded
);