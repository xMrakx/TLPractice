namespace Domain.Entities;

public class Property
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Country { get; private set; }
    public string City { get; private set; }
    public string Address { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public List<RoomType> RoomTypes { get; private set; }

    public Property(
        int id,
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude )
    {
        Id = id;
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    public void Update(
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude )
    {
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }
}
