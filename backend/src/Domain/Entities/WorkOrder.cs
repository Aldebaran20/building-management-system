using System.Text.Json.Serialization;

namespace BMS.Domain.Entities;

public class WorkOrder
{
    public long Id { get; set; }
    public long UserId { get; set; } // Foreign key to User
    public User User { get; set; } = null!; // Reference navigation to principal
    public long BuildingId { get; set; } // Foreign key to Building
    public Building Building { get; set; } = null!; // Reference navigation to principal
    public long ContractorId { get; set; } // Foreign key to Contractor
    public Contractor Contractor { get; set; } = null!; // Reference navigation to principal
    public required string Title { get; set; } 
    public string? Description { get; set; }
    public WorkOrderStatus Status { get; set; }
    public WorkOrderPriority? Priority { get; set; }
    public DateOnly DateCreated { get; set; }
    public DateOnly? Deadline { get; set; }
    public DateOnly? DateCompleted { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WorkOrderStatus
{
    Pending = 10,
    InProgress = 20,
    Completed = 30,
    Cancelled = 40,
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WorkOrderPriority
{
    Low = 10,
    Medium = 20,
    High = 30,
    Critical = 40,
}