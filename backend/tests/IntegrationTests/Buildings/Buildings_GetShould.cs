using BMS.Domain.Entities;
using BMS.Application.DTOs.Buildings;

namespace BMS.IntegrationTests.Buildings;

public sealed class Buildings_GetShould(PostgresFixture fixture)
:   IntegrationTestBase(fixture)
{
    [Fact]
    public async Task GetBuildings_MultipleExist_ReturnList()
    {
        // Arrange
        await ResetDatabaseAsync();
        await AuthenticateAsync();

        var postResponse1 = await _httpClient.PostAsJsonAsync("/api/buildings", new
        {
            BuildingName = "Building A",
            BuildingAddress = "123 Main St",
            NumberOfUnits = 1,
            BuildingType = BuildingType.Residential,
            BuildingStatus = BuildingStatus.Active
        });
        Assert.Equal(System.Net.HttpStatusCode.Created, postResponse1.StatusCode);
        Assert.Equal("application/json", postResponse1.Content.Headers.ContentType?.MediaType);


        var postResponse2 = await _httpClient.PostAsJsonAsync("/api/buildings", new
        {
            BuildingName = "Building B",
            BuildingAddress = "245 Main St",
            NumberOfUnits = 10,
            BuildingType = BuildingType.Commercial,
            BuildingStatus = BuildingStatus.Active
        });
        Assert.Equal(System.Net.HttpStatusCode.Created, postResponse2.StatusCode);
        Assert.Equal("application/json", postResponse2.Content.Headers.ContentType?.MediaType);


        // Act
        var getResponse = await _httpClient.GetAsync("/api/buildings");
        var buildings = await getResponse.Content.ReadFromJsonAsync<List<BuildingDTO>>();

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal("application/json", getResponse.Content.Headers.ContentType?.MediaType);

        Assert.NotNull(buildings);
        Assert.Equal(2, buildings.Count);
    }

    [Fact]
    public async Task GetBuildings_NoToken_ReturnsUnauthorized()
    {
        // Don't call AuthenticateAsync
        await ResetDatabaseAsync();
        
        var getResponse = await _httpClient.GetAsync("/api/buildings");
        
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, getResponse.StatusCode);
    }
}