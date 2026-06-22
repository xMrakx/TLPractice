using WebApi.DTOs.PropertyDTOs;
using WebApi.DTOs.RoomTypeDTOs;

namespace WebApi.DTOs.ReservationDTOs;

public record SearchResultDto(
    PropertyResponseDto Property,
    RoomTypeResponseDto RoomType
);

