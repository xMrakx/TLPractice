using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.PropertyDTOs;

public record PropertyAddDto (

    [Required( ErrorMessage = "Название отеля обязательно" )]
    [StringLength( 50, ErrorMessage = "Название отеля не может быть длиннее 50 символов" )]
    string Name,

    [Required( ErrorMessage = "Название страны обязательна" )]
    [StringLength( 50, ErrorMessage = "Название страны не может быть длиннее 50 символов" )]
    string Country,

    [Required( ErrorMessage = "Название города обязателен" )]
    [StringLength( 50, ErrorMessage = "Город не может быть длиннее 50 символов" )]
    string City,

    [Required( ErrorMessage = "Адрес обязателен" )]
    [StringLength( 200, ErrorMessage = "Адрес не может быть длиннее 200 символов" )]
    string Address,

    [Required( ErrorMessage = "Широта обязательна" )]
    [Range( -90, 90, ErrorMessage = "Широта должна быть между -90 и 90" )]
    double Latitude,

    [Required( ErrorMessage = "Долгота обязательна" )]
    [Range( -180, 180, ErrorMessage = "Долгота должна быть между -180 и 180" )]
    double Longitude
);

