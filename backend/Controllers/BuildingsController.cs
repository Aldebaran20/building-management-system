using Microsoft.AspNetCore.Mvc;
using BuildingManagementSystem.Services;
using BuildingManagementSystem.DTOs;

namespace BuildingManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BuildingsController : ControllerBase
{
    private readonly IBuildingService _buildingService;

    public BuildingsController(IBuildingService buildingService)
    {
        _buildingService = buildingService;
    }

    // GET: api/Buildings
    [HttpGet]
    [EndpointSummary("Get all buildings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BuildingDTO>>> GetBuildings()
    {
        var buildings = await _buildingService.GetAllBuildingsAsync();
        return Ok(buildings);
    }

    // GET: api/Buildings/5
    [HttpGet("{id}")]
    [EndpointSummary("Get a building by ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BuildingDTO>> GetBuilding(long id)
    {
        var building = await _buildingService.GetBuildingByIdAsync(id);

        if (building == null)
        {
            return NotFound($"Building with ID {id} does not exist.");
        }

        return building;
    }

    // POST: api/Buildings
    [HttpPost]
    [EndpointSummary("Create a new building")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BuildingDTO>> PostBuilding(SaveBuildingDTO building)
    {
        var newBuilding = await _buildingService.CreateBuildingAsync(building);

        return CreatedAtAction(nameof(GetBuilding), new { id = newBuilding.Id }, newBuilding);
    }

    // PUT: api/Buildings/5
    [HttpPut("{id}")]
    [EndpointSummary("Update an existing building")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutBuilding(long id, SaveBuildingDTO building)
    {
        var updated = await _buildingService.UpdateBuildingAsync(id, building);
        if (!updated)
        {
            return BadRequest($"Building with ID {id} could not be updated.");
        }

        return NoContent();
    }

    // DELETE: api/Buildings/5
    [HttpDelete("{id}")]
    [EndpointSummary("Delete a building by id")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBuilding(long id)
    {
        var deleted = await _buildingService.DeleteBuildingAsync(id);
        if (!deleted)
        {
            return NotFound($"Building with ID {id} does not exist.");
        }

        return NoContent();
    }

}
