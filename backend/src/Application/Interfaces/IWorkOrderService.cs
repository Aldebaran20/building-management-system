using BMS.Domain.Entities;
using BMS.Application.DTOs.WorkOrders;

namespace BMS.Application.Interfaces;

public interface IWorkOrderService
{
    public Task<IEnumerable<WorkOrderDTO>> GetAllWorkOrdersAsync(long userId);
    public Task<WorkOrderDTO?> GetWorkOrderByIdAsync(long workOrderId, long userId);
    public Task<WorkOrderDTO> CreateWorkOrderAsync(SaveWorkOrderDTO workOrderDto, long userId);
    public Task<bool> UpdateWorkOrderAsync(long workOrderId, SaveWorkOrderDTO workOrderDto, long userId);
    public Task<bool> UpdateWorkOrderStatusAsync(long workOrderId, WorkOrderStatus newStatus, long userId);
    public Task<bool> DeleteWorkOrderAsync(long workOrderId, long userId);
}