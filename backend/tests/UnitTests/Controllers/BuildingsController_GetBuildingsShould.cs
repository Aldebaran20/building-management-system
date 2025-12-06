using NSubstitute;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;
using BMS.Application.DTOs;
using BMS.Web.Controllers;

namespace BMS.UnitTests.Controllers;

public class BuildingsController_GetBuildingsShould
{
    [Fact]
    public async Task GetBuildings_Multiple_ReturnsListOfBuildings()
    {
        // Arrange
        var expectedData = new List<BuildingDTO>
        {
            new(
                10,
                "Building A",
                "123 Main St",
                10,
                BuildingType.Residential,
                BuildingStatus.Active,
                new DateOnly(2024, 1, 1)
            ),
            new(
                20,
                "Building B",
                "456 Elm St",
                20,
                BuildingType.Commercial,
                BuildingStatus.Active,
                new DateOnly(2024, 1, 1)
            )
        };

        var mockService = Substitute.For<IBuildingService>();
        mockService.GetAllBuildingsAsync().Returns(expectedData);
        var mockValidator = Substitute.For<IValidator<SaveBuildingDTO>>();
        var buildingsController = new BuildingsController(mockService, mockValidator);

        // Act
        var result = await buildingsController.GetBuildings();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedData = Assert.IsType<IEnumerable<BuildingDTO>>(okResult.Value, exactMatch: false);
        Assert.Same(expectedData, returnedData);

        await mockService.Received(1).GetAllBuildingsAsync();
    }

    [Fact]
    public async Task GetBuildings_Empty_ReturnsEmptyList()
    {
        // Arrange
        var expectedData = new List<BuildingDTO>();

        var mockService = Substitute.For<IBuildingService>();
        mockService.GetAllBuildingsAsync().Returns(expectedData);
        var mockValidator = Substitute.For<IValidator<SaveBuildingDTO>>();
        var buildingsController = new BuildingsController(mockService, mockValidator);

        // Act
        var result = await buildingsController.GetBuildings();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedData = Assert.IsType<IEnumerable<BuildingDTO>>(okResult.Value, exactMatch: false);
        Assert.Empty(returnedData);

        await mockService.Received(1).GetAllBuildingsAsync();
    }
}