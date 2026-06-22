using System.Data;

namespace Domain.Entities;

public class RoomType
{
    public int Id { get; private set; }
    public int PropertyId { get; private set; }
    public string Name { get; private set; }
    public int DailyPrice { get; private set; }
    public string Currency { get; private set; }
    public int MinPersonCount { get; private set; }
    public int MaxPersonCount { get; private set; }
    public string? Services { get; private set; }
    public string? Amenities { get; private set; }

    public RoomType(
        int id,
        int propertyId,
        string name,
        int dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        string? services,
        string? amenities )
    {
        Id = id;
        PropertyId = propertyId;
        Name = name;
        DailyPrice = dailyPrice;
        Currency = currency;
        MinPersonCount = minPersonCount;
        MaxPersonCount = maxPersonCount;
        Services = services;
        Amenities = amenities;
    }

    public void Update(
        string name,
        int dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        string? services,
        string? amenities )
    {
        Name = name;
        DailyPrice = dailyPrice;
        Currency = currency;
        MinPersonCount = minPersonCount;
        MaxPersonCount = maxPersonCount;
        Services = services;
        Amenities = amenities;
    }
}
