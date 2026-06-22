namespace Domain.Entities;

public class Reservation
{
    public int Id { get; private set; }
    public int PropertyId { get; private set; }
    public int RoomTypeId { get; private set; }
    public DateTime ArrivalDate { get; private set; }
    public DateTime DepartureDate { get; private set; }
    public string GuestName { get; private set; }
    public string GuestPhoneNumber { get; private set; }
    public int Total { get; private set; }
    public string Currency { get; private set; }

    public Reservation(
        int id,
        int propertyId,
        int roomTypeId,
        DateTime arrivalDate,
        DateTime departureDate,
        string guestName,
        string guestPhoneNumber,
        int total,
        string currency )
    {
        Id = id;
        PropertyId = propertyId;
        RoomTypeId = roomTypeId;
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
        GuestName = guestName;
        GuestPhoneNumber = guestPhoneNumber;
        Total = total;
        Currency = currency;
    }

    public void Update(
        DateTime arrivalDate,
        DateTime departureDate,
        string guestName,
        string guestPhoneNumber )
    {
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
        GuestName = guestName;
        GuestPhoneNumber = guestPhoneNumber;
    }
}
