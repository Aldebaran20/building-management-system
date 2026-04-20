using NSubstitute;
using BMS.Application.Services;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;
using BMS.Application.DTOs.Buildings;

namespace BMS.UnitTests.Services.Buildings;

public class BuildingService_GetBuildingByIdShould
{
    [Fact]
    public async Task GetBuildingById_Existing_ReturnsBuilding()
    {
        // Arrange
        var existingBuilding = new Building
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

        var expectedDto = new BuildingDTO
        (
            10,
            "Building A",
            "123 Main St",
            10,
            BuildingType.Residential,
            BuildingStatus.Active,
            new DateOnly(2024, 1, 1)
        );

        var mockRepository = Substitute.For<IBuildingRepository>();
        mockRepository.GetBuildingByIdAsync(10, 1)
            .Returns(existingBuilding);
        var buildingService = new BuildingService(mockRepository);

        // Act
        var result = await buildingService.GetBuildingByIdAsync(10, 1);

        // Assert
        Assert.Equal(expectedDto, result);

        await mockRepository.Received(1).GetBuildingByIdAsync(10, 1);
    }

    [Fact]
    public async Task GetBuildingById_NonExistent_ReturnsNull()
    {
        // Arrange
        var mockRepository = Substitute.For<IBuildingRepository>();
        mockRepository.GetBuildingByIdAsync(10, 1)
            .Returns((Building?)null);
        var buildingService = new BuildingService(mockRepository);

        // Act
        var result = await buildingService.GetBuildingByIdAsync(10, 1);

        // Assert
        Assert.Null(result);

        await mockRepository.Received(1).GetBuildingByIdAsync(10, 1);
    }
}