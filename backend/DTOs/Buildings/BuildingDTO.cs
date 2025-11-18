using BuildingManagementSystem.Persistence.Entities;

namespace BuildingManagementSystem.DTOs;

public record BuildingDTO(
    long Id,
    string BuildingName,
    string BuildingAddress,
    int NumberOfUnits,
    BuildingType BuildingType,
    BuildingStatus BuildingStatus,
    DateOnly DateAdded
);