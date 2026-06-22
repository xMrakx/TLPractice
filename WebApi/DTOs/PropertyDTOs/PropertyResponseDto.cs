namespace WebApi.DTOs.PropertyDTOs;

public record PropertyResponseDto(
    int Id,
    string Name,
    string Country,
    string City,
    string Address,
    double Latitude,
    double Longitude
);
