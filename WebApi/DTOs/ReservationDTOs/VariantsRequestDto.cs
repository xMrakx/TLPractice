using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.ReservationDTOs;

public record VariantsRequestDto(
    [Required( ErrorMessage = "Страна обязана для заполнения" )]
    [StringLength( 50, ErrorMessage = "Название страны не может превышать 50 символов" )]
    string Country,

    [Required( ErrorMessage = "Город обязателен для заполнения" )]
    [StringLength( 50, ErrorMessage = "Название города не может превышать 50 символов" )]
    string City,

    [Required( ErrorMessage = "Дата заезда обязательна для заполнения" )]
    [DataType( DataType.Date, ErrorMessage = "Неверный формат даты" )]
    DateTime ArrivalDate,

    [Required( ErrorMessage = "Дата отъезда обязательна для заполнения" )]
    [DataType( DataType.Date, ErrorMessage = "Неверный формат даты" )]
    DateTime DepartureDate,

    [Required( ErrorMessage = "Количество гостей обязательно для заполнения" )]
    [Range( 1, int.MaxValue, ErrorMessage = "Количество гостей должно быть положительным числом" )]
    int GuestCount,

    [Required( ErrorMessage = "Максимальная цена обязательна для заполнения" )]
    [Range( 1, double.MaxValue, ErrorMessage = "Максимальная цена должна быть положительным числом" )]
    int MaxPrice
);
