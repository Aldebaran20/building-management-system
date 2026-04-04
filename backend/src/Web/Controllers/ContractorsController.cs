using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BMS.Application.Interfaces;
using BMS.Application.DTOs.Contractors;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BMS.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
[Produces("application/json")]
public class ContractorsController : ControllerBase
{
    private readonly IContractorService _contractorService;
    private readonly IValidator<SaveContractorDTO> _validator;

    public ContractorsController(
        IContractorService contractorService,
        IValidator<SaveContractorDTO> validator
    ){
        _contractorService = contractorService;
        _validator = validator;
    }

    // GET: api/Contractors
    [HttpGet]
    [EndpointSummary("Get all contractors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContractorDTO>>> GetContractors()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var contractors = await _contractorService.GetAllContractorsAsync(userId);
        return Ok(contractors);
    }

    // GET: api/Contractors/5
    [HttpGet("{id}")]
    [EndpointSummary("Get a contractor by ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContractorDTO>> GetContractor(long id)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }
        
        var contractor = await _contractorService.GetContractorByIdAsync(id, userId);
        if (contractor == null)
        {
            return NotFound($"Contractor with ID {id} does not exist.");
        }

        return Ok(contractor);
    }

    // POST: api/Contractors
    [HttpPost]
    [EndpointSummary("Create a new contractor")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContractorDTO>> PostContractor(SaveContractorDTO contractorDto)
    {
        var result = await _validator.ValidateAsync(contractorDto);
        if (!result.IsValid)
        {
            return BadRequest(result.ToDictionary());
        }

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var newContractor = await _contractorService.CreateContractorAsync(contractorDto, userId);

        return CreatedAtAction(nameof(GetContractor), new { id = newContractor.Id }, newContractor);
    }

    // PUT: api/Contractors/5
    [HttpPut("{id}")]
    [EndpointSummary("Update an existing contractor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutContractor(long id, SaveContractorDTO contractorDto)
    {
        var result = await _validator.ValidateAsync(contractorDto);
        if (!result.IsValid)
        {
            return BadRequest(result.ToDictionary());
        }

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var updated = await _contractorService.UpdateContractorAsync(id, contractorDto, userId);
        if (!updated)
        {
            return NotFound($"Contractor with ID {id} does not exist.");
        }


        return NoContent();
    }

    // DELETE: api/Contractors/5
    [HttpDelete("{id}")]
    [EndpointSummary("Delete a contractor by id")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContractor(long id)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var deleted = await _contractorService.DeleteContractorAsync(id, userId);
        if (!deleted)
        {
            return NotFound($"Contractor with ID {id} does not exist.");
        }

        return NoContent();
    }
}
