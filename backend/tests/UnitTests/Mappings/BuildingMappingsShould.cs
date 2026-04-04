using BMS.Domain.Entities;
using BMS.Application.DTOs.Buildings;
using BMS.Application.Mappings;

namespace BMS.UnitTests.Mappings;

public class BuildingMappingsShould
{
    [Fact]
    public void MapToBuildingDto_ReturnsCorrectDto()
    {
        // Arrange
        var inputEntity = new Building
        {
            Id = 10,
            UserId = 1,
            BuildingName = "Building A",
            BuildingAddress = "123 Main St",
            NumberOfUnits = 10,
            BuildingType = BuildingType.Residential,
            BuildingStatus = BuildingStatus.Active,
            DateAdded = new DateOnly(2024, 1, 1)
        };

        var expectedDto = new BuildingDTO(
            10,
            "Building A",
            "123 Main St",
            10,
            BuildingType.Residential,
            BuildingStatus.Active,
            new DateOnly(2024, 1, 1)
        );

        // Act
        var result = inputEntity.MapToBuildingDto();

        // Assert
        Assert.Equal(expectedDto, result);
    }

    [Fact]
    public void MapToBuildingEntity_ReturnsCorrectEntity()
    {
        // Arrange
        var inputDto = new SaveBuildingDTO(
            "Building A",
            "123 Main St",
            10,
            BuildingType.Residential,
            BuildingStatus.Active
        );

        // Act
        var result = inputDto.MapToBuildingEntity(1);

        // Assert
        Assert.Equal(1, result.UserId);
        Assert.Equal("Building A", result.BuildingName);
        Assert.Equal("123 Main St", result.BuildingAddress);
        Assert.Equal(10, result.NumberOfUnits);
        Assert.Equal(BuildingType.Residential, result.BuildingType);
        Assert.Equal(BuildingStatus.Active, result.BuildingStatus);
        Assert.Equal(DateOnly.FromDateTime(DateTime.UtcNow), result.DateAdded);
    }
}