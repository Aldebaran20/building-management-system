using System.ComponentModel.DataAnnotations;
using BuildingManagementSystem.Data.Entities;

namespace BuildingManagementSystem.DTOs.Buildings;

public record SaveBuildingDTO(
    [StringLength(50)]
    string BuildingName,
    [StringLength(100)]
    string BuildingAddress,
    [Range(1, 10000)]
    int NumberOfUnits,
    BuildingType BuildingType,
    BuildingStatus BuildingStatus
);