namespace WebApi.DTOs.RoomTypeDTOs;

public record RoomTypeResponseDto(
    int id,
    int propertyId,
    string name,
    int dailyPrice,
    string currency,
    int minPersonCount,
    int maxPersonCount,
    string? services,
    string? amenities
);
