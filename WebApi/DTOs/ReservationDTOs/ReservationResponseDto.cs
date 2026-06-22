namespace WebApi.DTOs.ReservationDTOs;

public record ReservationResponseDto(
    int Id,
    int PropertyId,
    int RoomTypeId,
    DateTime ArrivalDate,
    DateTime DepartureDate,
    string GuestName,
    string GuestPhoneNumber,
    int Total,
    string Currency
);
