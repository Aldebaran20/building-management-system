using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;
using BMS.Application.DTOs.Buildings;
using BMS.Application.Validators;

namespace BMS.UnitTests.Controllers.Buildings;

public class BuildingsController_PutBuildingShould
{
    [Fact]
    public async Task PutBuilding_ValidBuilding_ReturnsNoContent()
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
        mockService.UpdateBuildingAsync(10, inputDto, 1)
            .Returns(true);
        var validator = new BuildingValidator();
        var buildingsController = BuildingsControllerFactory.Create(mockService, validator, userId: 1);

        // Act
        var result = await buildingsController.PutBuilding(10, inputDto);

        // Assert
        Assert.IsType<NoContentResult>(result);

        await mockService.Received(1)
            .UpdateBuildingAsync(10, inputDto, 1);
    }

    [Fact]
    public async Task PutBuilding_InvalidBuilding_ReturnsBadRequest()
    {
        // Arrange
        var invalidDto = new SaveBuildingDTO
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
        var result = await buildingsController.PutBuilding(10, invalidDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);

        await mockService.Received(0)
            .UpdateBuildingAsync(10, invalidDto, 1);
    }

    [Fact]
    public async Task PutBuilding_NonExistentBuilding_ReturnsNotFound()
    {
        // Arrange
        var inputDto = new SaveBuildingDTO
        (
            "Building A",
            "123 Main St",
            1,
            BuildingType.Residential,
            BuildingStatus.Active
        );

        var mockService = Substitute.For<IBuildingService>();
        mockService.UpdateBuildingAsync(10, inputDto, 1)
            .Returns(false);
        var validator = new BuildingValidator();
        var buildingsController = BuildingsControllerFactory.Create(mockService, validator, userId: 1);

        // Act
        var result = await buildingsController.PutBuilding(10, inputDto);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);

        await mockService.Received(1)
            .UpdateBuildingAsync(10, inputDto, 1);
    }

    [Fact]
    public async Task PutBuilding_InvalidUserId_ReturnsUnauthorized()
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
        mockService.UpdateBuildingAsync(10, inputDto, 1)
            .Returns(true);
        var validator = new BuildingValidator();
        var buildingsController = BuildingsControllerFactory.Create(mockService, validator);

        // Act
        var result = await buildingsController.PutBuilding(10, inputDto);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);

        await mockService.DidNotReceive()
            .UpdateBuildingAsync(Arg.Any<long>(), Arg.Any<SaveBuildingDTO>(), Arg.Any<long>());
    }
}