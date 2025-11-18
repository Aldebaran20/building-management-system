using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BuildingManagementSystem.Persistence.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BuildingManagementSystem.DTOs;

public record SaveBuildingDTO(
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Building name must be between 3 and 50 characters.")]
    string BuildingName,
    [StringLength(100, MinimumLength = 10, ErrorMessage = "Building address must be between 10 and 100 characters.")]
    string BuildingAddress,
    [Range(1, 10000, ErrorMessage = "Number of units must be greater than 0.")]
    int NumberOfUnits,
    [EnumDataType(typeof(BuildingType), ErrorMessage = "Invalid building type.")]
    BuildingType BuildingType,
    [EnumDataType(typeof(BuildingStatus), ErrorMessage = "Invalid building status.")]
    BuildingStatus BuildingStatus
);