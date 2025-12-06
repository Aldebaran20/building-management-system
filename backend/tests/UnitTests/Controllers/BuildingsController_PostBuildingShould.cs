using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;
using BMS.Application.DTOs;
using BMS.Web.Controllers;
using BMS.Application.Validators;

namespace BMS.UnitTests.Controllers;

public class BuildingsController_PostBuildingShould
{
    [Fact]
    public async Task PostBuilding_ValidBuilding_ReturnsCreatedResult()
    {
        // Arrange
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

        var mockService = Substitute.For<IBuildingService>();
        mockService.CreateBuildingAsync(inputDto)
            .Returns(expectedDto);
        var validator = new BuildingValidator();
        var buildingsController = new BuildingsController(mockService, validator);


        // Act
        var result = await buildingsController.PostBuilding(inputDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedDto = Assert.IsType<BuildingDTO>(createdResult.Value, exactMatch: false);
        Assert.Same(expectedDto, returnedDto);

        await mockService.Received(1)
            .CreateBuildingAsync(inputDto);
    }

    [Fact]
    public async Task PostBuilding_InvalidBuilding_ReturnsBadRequest()
    {
        // Arrange
        var inputDto = new SaveBuildingDTO
        (
            "2",
            "shrt",
            0,
            BuildingType.None,
            BuildingStatus.None
        );

        var mockService = Substitute.For<IBuildingService>();
        var validator = new BuildingValidator();
        var buildingsController = new BuildingsController(mockService, validator);


        // Act
        var result = await buildingsController.PostBuilding(inputDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);

        await mockService.Received(0)
            .CreateBuildingAsync(inputDto);
    }
}