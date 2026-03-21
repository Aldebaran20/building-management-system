using NSubstitute;
using BMS.Application.Services;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;
using BMS.Application.DTOs;

namespace BMS.UnitTests.Services;

public class BuildingService_GetAllBuildingsShould
{
    [Fact]
    public async Task GetAllBuildings_Multiple_ReturnsListOfBuildings()
    {
        // Arrange
        var data = new List<Building>
        {
            new() {
                Id = 10,
                UserId = 1,
                BuildingName = "Building A",
                BuildingAddress = "123 Main St",
                NumberOfUnits = 10,
                BuildingType = BuildingType.Residential,
                BuildingStatus = BuildingStatus.Active,
                DateAdded = new DateOnly(2024, 1, 1)
            },
            new() {
                Id = 20,
                UserId = 1,
                BuildingName = "Building B",
                BuildingAddress = "456 Elm St",
                NumberOfUnits = 20,
                BuildingType = BuildingType.Commercial,
                BuildingStatus = BuildingStatus.Active,
                DateAdded = new DateOnly(2024, 1, 1)
            }
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
        mockRepository.GetAllBuildingsAsync(1).Returns(data);
        var buildingService = new BuildingService(mockRepository);

        // Act
        var result = await buildingService.GetAllBuildingsAsync(1);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(expectedDto, result.First());

        await mockRepository.Received(1).GetAllBuildingsAsync(Arg.Any<long>());
    }

    [Fact]
    public async Task GetAllBuildings_Empty_ReturnsEmptyList()
    {
        // Arrange
        var data = new List<Building>();

        var mockRepository = Substitute.For<IBuildingRepository>();
        mockRepository.GetAllBuildingsAsync(1).Returns(data);
        var buildingService = new BuildingService(mockRepository);

        // Act
        var result = await buildingService.GetAllBuildingsAsync(1);

        // Assert
        Assert.Empty(result);

        await mockRepository.Received(1).GetAllBuildingsAsync(Arg.Any<long>());
    }
}