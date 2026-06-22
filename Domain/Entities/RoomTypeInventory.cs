namespace Domain.Entities;

public class RoomTypeInventory
{
    public int Id { get; private set; }
    public int RoomTypeId { get; private set; }
    public int RoomCount { get; private set; }

    public RoomTypeInventory( int roomTypeId, int roomCount )
    {
        RoomTypeId = roomTypeId;
        RoomCount = roomCount;
    }

    public void Update( int newCount )
    {
        RoomCount = newCount;
    }
}
