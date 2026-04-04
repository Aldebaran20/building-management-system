using BMS.Domain.Entities;
using BMS.Application.DTOs.Buildings;

namespace BMS.IntegrationTests.Buildings;

public sealed class Buildings_DeleteShould(PostgresFixture fixture)
:   IntegrationTestBase(fixture)
{
    [Fact]
    public async Task DeleteBuilding_Exists_ReturnsNoContent()
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
        var deleteResponse = await _httpClient.DeleteAsync($"/api/buildings/{created.Id}");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var getResponse = await _httpClient.GetAsync($"/api/buildings/{created.Id}");
        Assert.Equal(System.Net.HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteBuilding_NonExistent_ReturnsNotFound()
    {
        // Arrange
        await ResetDatabaseAsync();
        await AuthenticateAsync();

        // Act
        var deleteResponse = await _httpClient.DeleteAsync("/api/buildings/9999");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, deleteResponse.StatusCode);
    }
}