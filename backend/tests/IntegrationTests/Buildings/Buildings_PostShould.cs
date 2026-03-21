using BMS.Domain.Entities;
using BMS.Application.DTOs;

namespace BMS.IntegrationTests.Buildings;

public sealed class Buildings_PostShould(PostgresFixture fixture)
:   IntegrationTestBase(fixture)
{
    [Fact]
    public async Task PostBuilding_Valid_ReturnsCreated()
    {
        // Arrange
        await ResetDatabaseAsync();
        await AuthenticateAsync();

        // Act
        var postResponse = await _httpClient.PostAsJsonAsync("/api/buildings", new
        {
            BuildingName = "Building A",
            BuildingAddress = "123 Main St",
            NumberOfUnits = 1,
            BuildingType = BuildingType.Residential,
            BuildingStatus = BuildingStatus.Active
        });
        var created = await postResponse.Content.ReadFromJsonAsync<BuildingDTO>();

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Created, postResponse.StatusCode);
        Assert.Equal("application/json", postResponse.Content.Headers.ContentType?.MediaType);

        Assert.NotNull(created);
        Assert.Equal("Building A", created.BuildingName);
        Assert.Equal("123 Main St", created.BuildingAddress);
        Assert.Equal(1, created.NumberOfUnits);
        Assert.Equal(BuildingType.Residential, created.BuildingType);
        Assert.Equal(BuildingStatus.Active, created.BuildingStatus);

        var getResponse = await _httpClient.GetAsync($"/api/buildings/{created.Id}");
        Assert.Equal(System.Net.HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal("application/json", getResponse.Content.Headers.ContentType?.MediaType);

        var building = await getResponse.Content.ReadFromJsonAsync<BuildingDTO>();
        Assert.NotNull(building);
        Assert.Equal(created.Id, building.Id);
        Assert.Equal(created.BuildingName, building.BuildingName);
        Assert.Equal(created.BuildingAddress, building.BuildingAddress);
        Assert.Equal(created.NumberOfUnits, building.NumberOfUnits);
        Assert.Equal(created.BuildingType, building.BuildingType);
        Assert.Equal(created.BuildingStatus, building.BuildingStatus);
    }

    [Fact]
    public async Task PostBuilding_Invalid_ReturnsBadRequest()
    {
        // Arrange
        await ResetDatabaseAsync();
        await AuthenticateAsync();

        // Act
        var postResponse = await _httpClient.PostAsJsonAsync("/api/buildings", new
        {
            BuildingName = "A",
            BuildingAddress = "1",
            NumberOfUnits = 0,
            BuildingType = BuildingType.None,
            BuildingStatus = BuildingStatus.None
        });
        var error = await postResponse.Content.ReadFromJsonAsync<Dictionary<string, string[]>>();

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, postResponse.StatusCode);
        Assert.Equal("application/json", postResponse.Content.Headers.ContentType?.MediaType);

        Assert.NotNull(error);
        Assert.Contains("BuildingName", error.Keys);
        Assert.Contains("BuildingAddress", error.Keys);
        Assert.Contains("NumberOfUnits", error.Keys);
        Assert.Contains("BuildingType", error.Keys);
        Assert.Contains("BuildingStatus", error.Keys);

        foreach (var msg in error.Values)
        {
            Assert.NotEmpty(msg);
        }
    }
}