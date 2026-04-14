using Microsoft.EntityFrameworkCore;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;

namespace BMS.Infrastructure.Repositories;

public class WorkOrderRepository : IWorkOrderRepository
{

    private readonly ApplicationDbContext _context;

    public WorkOrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkOrder>> GetAllWorkOrdersAsync(long userId)
    {
        return await _context.WorkOrders
            .Include(w => w.Building)
            .Include(w => w.Contractor)
            .Where(wo => wo.UserId == userId).ToListAsync();
    }

    public async Task<WorkOrder?> GetWorkOrderByIdAsync(long workOrderId, long userId)
    {
        return await _context.WorkOrders
            .Include(w => w.Building)
            .Include(w => w.Contractor)
            .FirstOrDefaultAsync(wo => wo.Id == workOrderId && wo.UserId == userId);
    }

    public async Task<WorkOrder> CreateWorkOrderAsync(WorkOrder workOrder)
    {
        _context.WorkOrders.Add(workOrder);
        await _context.SaveChangesAsync();
        return await _context.WorkOrders
            .Include(w => w.Building)
            .Include(w => w.Contractor)
            .FirstAsync(w => w.Id == workOrder.Id);
    }

    public async Task UpdateWorkOrderAsync(WorkOrder workOrder)
    {
        _context.Entry(workOrder).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWorkOrderAsync(WorkOrder workOrder)
    {
        _context.WorkOrders.Remove(workOrder);
        await _context.SaveChangesAsync();
    }
}