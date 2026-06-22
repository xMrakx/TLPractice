namespace Domain.DTOs;

public record VariantsFilter(
    string Country,
    string city,
    DateTime arrivalDate,
    DateTime departureDate,
    int guestCount,
    int maxPrice
);
