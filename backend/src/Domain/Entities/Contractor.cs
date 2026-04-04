using System.Text.Json.Serialization;

namespace BMS.Domain.Entities;

public class Contractor
{
    public long Id { get; set; }
    public long UserId { get; set; } // Foreign key to User
    public User User { get; set; } = null!; // Reference navigation to principal
    public string? BusinessName { get; set; }
    public string? ContactName { get; set; }
    public string? ContactEmail { get; set; }
    public required string ContactPhone { get; set; }
    public string? AreaOfOperations { get; set; }
    public ContractorType ContractorType { get; set; }
    public ContractorStatus ContractorStatus { get; set; }
    public DateOnly DateAdded { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ContractorType
{
    Electrical = 10,
    Plumbing = 20,
    Carpentry = 30,
    Painting = 40,
    Roofing = 50,
    HVAC = 60,
    Landscaping = 70,
    Cleaning = 80,
    General = 90,
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ContractorStatus
{
    Available = 10,
    Unavailable = 20,
    Inactive = 30,
}