namespace Domain.DTOs;

public record ReservationFilter(
    int? PropertyId = null,
    int? RoomTypeId = null,
    DateTime? ArrivalDate = null,
    DateTime? DepartureDate = null,
    string? GuestName = null,
    string? Country = null,
    string? City = null
);
