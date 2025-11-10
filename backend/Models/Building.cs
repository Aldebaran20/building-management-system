namespace BuildingManagementSystem.Models;

public class Building
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public int NumUnits { get; set; }

    public BuildingType Type { get; set; }

    public BuildingStatus Status { get; set; }

    public DateOnly? DateAdded { get; set; }
}

public enum BuildingType
{
    None = 0,
    Residential = 10,
    Commercial = 20,
    Industrial = 30,
    MixedUse = 40,
}

public enum BuildingStatus
{
    None = 0,
    Active = 10,
    UnderConstruction = 20,
}