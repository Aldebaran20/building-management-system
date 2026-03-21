using BMS.Domain.Entities;
using BMS.Application.DTOs;

namespace BMS.IntegrationTests.Buildings;

public sealed class Buildings_PutShould(PostgresFixture fixture)
:   IntegrationTestBase(fixture)
{
    [Fact]
    public async Task PutBuilding_Valid_ReturnsNoContent()
    {
        // Arrange
        await ResetDatabaseAsync();
        await AuthenticateAsync();

        var postResponse = await _httpClient.PostAsJsonAsync("/api/buildings", new
        {
            BuildingName = "Building A",
            BuildingAddress = "123 Main St",
            NumberOfUnits = 1,
            BuildingType = BuildingType.Residential,
            BuildingStatus = BuildingStatus.Active
        });
        Assert.Equal(System.Net.HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<BuildingDTO>();
        Assert.NotNull(created);

        // Act
        var putResponse = await _httpClient.PutAsJsonAsync($"/api/buildings/{created.Id}", new
        {
            BuildingName = "Building B",
            BuildingAddress = "245 Main St",
            NumberOfUnits = 10,
            BuildingType = BuildingType.Commercial,
            BuildingStatus = BuildingStatus.UnderConstruction
        });

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NoContent, putResponse.StatusCode);

        var getResponse = await _httpClient.GetAsync($"/api/buildings/{created.Id}");
        Assert.Equal(System.Net.HttpStatusCode.OK, getResponse.StatusCode);

        var building = await getResponse.Content.ReadFromJsonAsync<BuildingDTO>();
        Assert.NotNull(building);
        Assert.Equal(created.Id, building.Id);
        Assert.Equal("Building B", building.BuildingName);
        Assert.Equal("245 Main St", building.BuildingAddress);
        Assert.Equal(10, building.NumberOfUnits);
        Assert.Equal(BuildingType.Commercial, building.BuildingType);
        Assert.Equal(BuildingStatus.UnderConstruction, building.BuildingStatus);
    }

    [Fact]
    public async Task PutBuilding_Invalid_ReturnsBadRequest()
    {
        // Arrange
        await ResetDatabaseAsync();
        await AuthenticateAsync();

        var postResponse = await _httpClient.PostAsJsonAsync("/api/buildings", new
        {
            BuildingName = "Building A",
            BuildingAddress = "123 Main St",
            NumberOfUnits = 1,
            BuildingType = BuildingType.Residential,
            BuildingStatus = BuildingStatus.Active
        });
        Assert.Equal(System.Net.HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<BuildingDTO>();
        Assert.NotNull(created);

        // Act
        var putResponse = await _httpClient.PutAsJsonAsync($"/api/buildings/{created.Id}", new
        {
            BuildingName = "A",
            BuildingAddress = "1",
            NumberOfUnits = 0,
            BuildingType = BuildingType.None,
            BuildingStatus = BuildingStatus.None
        });

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, putResponse.StatusCode);
        Assert.Equal("application/json", putResponse.Content.Headers.ContentType?.MediaType);

        var error = await putResponse.Content.ReadFromJsonAsync<Dictionary<string, string[]>>();
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

    [Fact]
    public async Task PutBuilding_NonExistent_ReturnsNotFound()
    {
        // Arrange
        await ResetDatabaseAsync();
        await AuthenticateAsync();

        // Act
        var putResponse = await _httpClient.PutAsJsonAsync($"/api/buildings/9999", new
        {
            BuildingName = "Building B",
            BuildingAddress = "245 Main St",
            NumberOfUnits = 10,
            BuildingType = BuildingType.Commercial,
            BuildingStatus = BuildingStatus.UnderConstruction
        });

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, putResponse.StatusCode);
    }
}