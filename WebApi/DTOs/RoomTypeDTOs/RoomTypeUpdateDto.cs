using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.RoomTypeDTOs;

public record RoomTypeUpdateDto(
    [Required( ErrorMessage = "Название номера обязательно" )]
    [StringLength( 50, ErrorMessage = "Название номера не может быть длинее 50 символов" )]
    string Name,

    [Required( ErrorMessage = "Цена обязательна" )]
    [Range( 1, int.MaxValue, ErrorMessage = "Цена должна быть больше 0" )]
    int DailyPrice,

    [Required( ErrorMessage = "Вид валюты обязателен" )]
    string Currency,

    [Required( ErrorMessage = "Минимальное количество гостей обязательно" )]
    [Range( 1, int.MaxValue, ErrorMessage = "Минимальное количество гостей должно быть больше 0" )]
    int MinPersonCount,

    [Required( ErrorMessage = "Максимальное количество гостей обязательно" )]
    [Range( 1, int.MaxValue, ErrorMessage = "Максимальное количество гостей должно быть больше 0" )]
    int MaxPersonCount,

    string? Services,

    string? Amenities
);
