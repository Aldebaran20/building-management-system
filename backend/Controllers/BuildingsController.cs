using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildingManagementSystem.Services;
using BuildingManagementSystem.DTOs.Buildings;
using BuildingManagementSystem.Data.Entities;

namespace BuildingManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuildingsController : ControllerBase
{
    private readonly IBuildingService _buildingService;

    public BuildingsController(IBuildingService buildingService)
    {
        _buildingService = buildingService;
    }

    // GET: api/Buildings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BuildingDTO>>> GetBuildings()
    {
        var buildings = await _buildingService.GetAllBuildingsAsync();
        return Ok(buildings);
    }

    // GET: api/Buildings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BuildingDTO>> GetBuilding(long id)
    {
        var building = await _buildingService.GetBuildingByIdAsync(id);

        if (building == null)
        {
            return NotFound();
        }

        return building;
    }

    // POST: api/Buildings
    [HttpPost]
    public async Task<ActionResult<Building>> PostBuilding(SaveBuildingDTO building)
    {
        var newBuilding = await _buildingService.CreateBuildingAsync(building);

        return CreatedAtAction(nameof(GetBuilding), new { id = newBuilding.Id }, newBuilding);
    }

    // PUT: api/Buildings/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBuilding(long id, SaveBuildingDTO building)
    {
        try
        {
            await _buildingService.UpdateBuildingAsync(id, building);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _buildingService.BuildingExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Buildings/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBuilding(long id)
    {
        var building = await _buildingService.GetBuildingByIdAsync(id);
        if (building == null)
        {
            return NotFound();
        }

        await _buildingService.DeleteBuildingAsync(id);

        return NoContent();
    }

}
