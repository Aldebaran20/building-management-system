using BMS.Application.Mappings;
using BMS.Application.Interfaces;
using BMS.Application.DTOs.WorkOrders;
using BMS.Domain.Entities;

namespace BMS.Application.Services;

public class WorkOrderService : IWorkOrderService
{

    private readonly IWorkOrderRepository _repository;

    public WorkOrderService(IWorkOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<WorkOrderDTO>> GetAllWorkOrdersAsync(long userId)
    {
        var workOrders = await _repository.GetAllWorkOrdersAsync(userId);
        return workOrders.Select(wo => wo.MapToWorkOrderDto());
    }

    public async Task<WorkOrderDTO?> GetWorkOrderByIdAsync(long workOrderId, long userId)
    {
        var workOrder = await _repository.GetWorkOrderByIdAsync(workOrderId, userId);
        return workOrder?.MapToWorkOrderDto();
    }

    public async Task<WorkOrderDTO> CreateWorkOrderAsync(SaveWorkOrderDTO workOrderDto, long userId)
    {
        var newWorkOrder = workOrderDto.MapToWorkOrderEntity(userId);

        var addedWorkOrder = await _repository.CreateWorkOrderAsync(newWorkOrder);

        return addedWorkOrder.MapToWorkOrderDto();
    }

    public async Task<bool> UpdateWorkOrderAsync(long workOrderId, SaveWorkOrderDTO workOrderDto, long userId)
    {
        var existingWorkOrder = await _repository.GetWorkOrderByIdAsync(workOrderId, userId);
        if (existingWorkOrder == null)
        {
            return false;
        }

        existingWorkOrder.BuildingId = workOrderDto.BuildingId;
        existingWorkOrder.ContractorId = workOrderDto.ContractorId;
        existingWorkOrder.Title = workOrderDto.Title;
        existingWorkOrder.Description = workOrderDto.Description;
        existingWorkOrder.Priority = workOrderDto.Priority;
        existingWorkOrder.Deadline = workOrderDto.Deadline;

        await _repository.UpdateWorkOrderAsync(existingWorkOrder);
        return true;
    }

    public async Task<bool> UpdateWorkOrderStatusAsync(long workOrderId, WorkOrderStatus newStatus, long userId)
    {
        var existingWorkOrder = await _repository.GetWorkOrderByIdAsync(workOrderId, userId);
        if (existingWorkOrder == null)
        {
            return false;
        }

        existingWorkOrder.Status = newStatus;
        existingWorkOrder.DateCompleted = newStatus == WorkOrderStatus.Completed 
            ? DateOnly.FromDateTime(DateTime.UtcNow) 
            : null;

        await _repository.UpdateWorkOrderAsync(existingWorkOrder);
        return true;
    }

    public async Task<bool> DeleteWorkOrderAsync(long workOrderId, long userId)
    {
        var existingWorkOrder = await _repository.GetWorkOrderByIdAsync(workOrderId, userId);
        if (existingWorkOrder == null)
        {
            return false;
        }

        await _repository.DeleteWorkOrderAsync(existingWorkOrder);
        return true;
    }
}