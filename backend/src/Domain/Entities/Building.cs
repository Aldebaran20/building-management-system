using System.Text.Json.Serialization;

namespace BMS.Domain.Entities;

public class Building
{
    public long Id { get; set; }
    public long UserId { get; set; } // Foreign key to User
    public User User { get; set; } = null!; // Reference navigation to principal
    public required string BuildingName { get; set; }
    public required string BuildingAddress { get; set; }
    public int NumberOfUnits { get; set; }
    public BuildingType BuildingType { get; set; }
    public BuildingStatus BuildingStatus { get; set; }
    public DateOnly DateAdded { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BuildingType
{
    None = 0,
    Residential = 10,
    Commercial = 20,
    Industrial = 30,
    MixedUse = 40,
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BuildingStatus
{
    None = 0,
    Active = 10,
    UnderConstruction = 20,
}