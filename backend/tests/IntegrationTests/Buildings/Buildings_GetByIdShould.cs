using BMS.Domain.Entities;
using BMS.Application.DTOs;

namespace BMS.IntegrationTests.Buildings;

public sealed class Buildings_GetByIdShould(PostgresFixture fixture)
:   IntegrationTestBase(fixture)
{
    [Fact]
    public async Task GetBuilding_Exists_ReturnsOk()
    {
        // Arrange
        await ResetDatabaseAsync();

        var postResponse = await _httpClient.PostAsJsonAsync("/api/buildings", new
        {
            BuildingName = "Building A",
            BuildingAddress = "123 Main St",
            NumberOfUnits = 1,
            BuildingType = BuildingType.Residential,
            BuildingStatus = BuildingStatus.Active
        });
        Assert.Equal(System.Net.HttpStatusCode.Created, postResponse.StatusCode);
        Assert.Equal("application/json", postResponse.Content.Headers.ContentType?.MediaType);

        var created = await postResponse.Content.ReadFromJsonAsync<BuildingDTO>();
        Assert.NotNull(created);

        // Act
        var getResponse = await _httpClient.GetAsync($"/api/buildings/{created.Id}");
        var building = await getResponse.Content.ReadFromJsonAsync<BuildingDTO>();

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal("application/json", getResponse.Content.Headers.ContentType?.MediaType);

        Assert.NotNull(building);
        Assert.Equal(created.Id, building.Id);
    }

    [Fact]
    public async Task GetBuilding_NonExistent_ReturnsNotFound()
    {
        // Arrange
        await ResetDatabaseAsync();

        // Act
        var getResponse = await _httpClient.GetAsync("/api/buildings/9999");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}