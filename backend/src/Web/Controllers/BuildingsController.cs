using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BMS.Application.Interfaces;
using BMS.Application.DTOs;

namespace BMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BuildingsController : ControllerBase
{
    private readonly IBuildingService _buildingService;
    private readonly IValidator<SaveBuildingDTO> _validator;

    public BuildingsController(
        IBuildingService buildingService,
        IValidator<SaveBuildingDTO> validator
    ){
        _buildingService = buildingService;
        _validator = validator;
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

        return Ok(building);
    }

    // POST: api/Buildings
    [HttpPost]
    [EndpointSummary("Create a new building")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BuildingDTO>> PostBuilding(SaveBuildingDTO building)
    {
        var result = await _validator.ValidateAsync(building);

        if (!result.IsValid)
        {
            return BadRequest(result.ToDictionary());
        }

        var newBuilding = await _buildingService.CreateBuildingAsync(building);

        return CreatedAtAction(nameof(GetBuilding), new { id = newBuilding.Id }, newBuilding);
    }

    // PUT: api/Buildings/5
    [HttpPut("{id}")]
    [EndpointSummary("Update an existing building")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutBuilding(long id, SaveBuildingDTO building)
    {
        var result = await _validator.ValidateAsync(building);

        if (!result.IsValid)
        {
            return BadRequest(result.ToDictionary());
        }

        var updated = await _buildingService.UpdateBuildingAsync(id, building);
        if (!updated)
        {
            return NotFound($"Building with ID {id} does not exist.");
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
