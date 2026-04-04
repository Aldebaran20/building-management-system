using NSubstitute;
using BMS.Application.Services;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;
using BMS.Application.DTOs.Buildings;

namespace BMS.UnitTests.Services;

public class BuildingService_CreateBuildingShould
{
    [Fact]
    public async Task CreateBuilding_ValidBuilding_ReturnsCreatedBuilding()
    {
        // Arrange
        var returnedEntity = new Building
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

        var inputDto = new SaveBuildingDTO
        (
            "Building A",
            "123 Main St",
            10,
            BuildingType.Residential,
            BuildingStatus.Active
        );

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
        mockRepository.CreateBuildingAsync(Arg.Any<Building>())
            .Returns(returnedEntity);
        var buildingService = new BuildingService(mockRepository);

        // Act
        var result = await buildingService.CreateBuildingAsync(inputDto, 1);

        // Assert
        Assert.Equal(expectedDto, result);

        await mockRepository.Received(1)
            .CreateBuildingAsync(Arg.Is<Building>(b => b.UserId == 1));
    }   
}