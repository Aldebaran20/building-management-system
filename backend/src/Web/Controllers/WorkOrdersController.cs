using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BMS.Application.Interfaces;
using BMS.Application.DTOs.WorkOrders;
using BMS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BMS.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
[Produces("application/json")]
public class WorkOrdersController : ControllerBase
{
    private readonly IWorkOrderService _workOrderService;
    private readonly IValidator<SaveWorkOrderDTO> _validator;

    public WorkOrdersController(
        IWorkOrderService workOrderService,
        IValidator<SaveWorkOrderDTO> validator
    ){
        _workOrderService = workOrderService;
        _validator = validator;
    }

    // GET: api/WorkOrders
    [HttpGet]
    [EndpointSummary("Get all work orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WorkOrderDTO>>> GetWorkOrders()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var workOrders = await _workOrderService.GetAllWorkOrdersAsync(userId);
        return Ok(workOrders);
    }

    // GET: api/WorkOrders/5
    [HttpGet("{id}")]
    [EndpointSummary("Get a work order by ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WorkOrderDTO>> GetWorkOrder(long id)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }
        
        var workOrder = await _workOrderService.GetWorkOrderByIdAsync(id, userId);
        if (workOrder == null)
        {
            return NotFound($"WorkOrder with ID {id} does not exist.");
        }

        return Ok(workOrder);
    }

    // POST: api/WorkOrders
    [HttpPost]
    [EndpointSummary("Create a new work order")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WorkOrderDTO>> PostWorkOrder(SaveWorkOrderDTO workOrderDto)
    {
        var result = await _validator.ValidateAsync(workOrderDto);
        if (!result.IsValid)
        {
            return BadRequest(result.ToDictionary());
        }

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var newWorkOrder = await _workOrderService.CreateWorkOrderAsync(workOrderDto, userId);

        return CreatedAtAction(nameof(GetWorkOrder), new { id = newWorkOrder.Id }, newWorkOrder);
    }

    // PUT: api/WorkOrders/5
    [HttpPut("{id}")]
    [EndpointSummary("Update an existing work order")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutWorkOrder(long id, SaveWorkOrderDTO workOrderDto)
    {
        var result = await _validator.ValidateAsync(workOrderDto);
        if (!result.IsValid)
        {
            return BadRequest(result.ToDictionary());
        }

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var updated = await _workOrderService.UpdateWorkOrderAsync(id, workOrderDto, userId);
        if (!updated)
        {
            return NotFound($"WorkOrder with ID {id} does not exist.");
        }


        return NoContent();
    }

    // PATCH: api/WorkOrders/5/status
    [HttpPatch("{id}/status")]
    [EndpointSummary("Update an existing work order's status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchWorkOrderStatus(long id, [FromBody] WorkOrderStatus newStatus)
    {
        if (!Enum.IsDefined(newStatus))
        {
            return BadRequest("Invalid work order status.");
        }

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var updated = await _workOrderService.UpdateWorkOrderStatusAsync(id, newStatus, userId);
        if (!updated)
        {
            return NotFound($"WorkOrder with ID {id} does not exist.");
        }


        return NoContent();
    }

    // DELETE: api/WorkOrders/5
    [HttpDelete("{id}")]
    [EndpointSummary("Delete a work order by id")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWorkOrder(long id)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var deleted = await _workOrderService.DeleteWorkOrderAsync(id, userId);
        if (!deleted)
        {
            return NotFound($"WorkOrder with ID {id} does not exist.");
        }

        return NoContent();
    }
}
