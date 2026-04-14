namespace BMS.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public required string Email { get; set; }
    public required string HashedPassword { get; set; }
    public ICollection<Building> Buildings { get; } = new List<Building>();
    public ICollection<Contractor> Contractors { get; } = new List<Contractor>();
    public ICollection<WorkOrder> WorkOrders { get; } = new List<WorkOrder>();
}