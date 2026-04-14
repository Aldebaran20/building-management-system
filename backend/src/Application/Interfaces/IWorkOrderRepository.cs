using BMS.Domain.Entities;

namespace BMS.Application.Interfaces;

public interface IWorkOrderRepository
{
    public Task<IEnumerable<WorkOrder>> GetAllWorkOrdersAsync(long userId);
    public Task<WorkOrder?> GetWorkOrderByIdAsync(long workOrderId, long userId);
    public Task<WorkOrder> CreateWorkOrderAsync(WorkOrder workOrder);
    public Task UpdateWorkOrderAsync(WorkOrder workOrder);
    public Task DeleteWorkOrderAsync(WorkOrder workOrder);
}