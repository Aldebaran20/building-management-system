using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;
using BMS.Application.DTOs.Buildings;
using BMS.Application.Validators;

namespace BMS.UnitTests.Controllers.Buildings;

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
        mockService.CreateBuildingAsync(inputDto, 1)
            .Returns(expectedDto);
        var validator = new BuildingValidator();
        var buildingsController = BuildingsControllerFactory.Create(mockService, validator, userId: 1);


        // Act
        var result = await buildingsController.PostBuilding(inputDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedDto = Assert.IsType<BuildingDTO>(createdResult.Value, exactMatch: false);
        Assert.Same(expectedDto, returnedDto);

        await mockService.Received(1)
            .CreateBuildingAsync(inputDto, 1);
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
            BuildingType.Residential,
            BuildingStatus.Active
        );

        var mockService = Substitute.For<IBuildingService>();
        var validator = new BuildingValidator();
        var buildingsController = BuildingsControllerFactory.Create(mockService, validator, userId: 1);


        // Act
        var result = await buildingsController.PostBuilding(inputDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);

        await mockService.DidNotReceive()
            .CreateBuildingAsync(inputDto, 1);
    }

    [Fact]
    public async Task PostBuilding_InvalidUserId_ReturnsUnauthorized()
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

        var mockService = Substitute.For<IBuildingService>();
        var validator = new BuildingValidator();
        var buildingsController = BuildingsControllerFactory.Create(mockService, validator);


        // Act
        var result = await buildingsController.PostBuilding(inputDto);

        // Assert
        var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result.Result);

        await mockService.DidNotReceive()
            .CreateBuildingAsync(Arg.Any<SaveBuildingDTO>(), Arg.Any<long>());
    }
}