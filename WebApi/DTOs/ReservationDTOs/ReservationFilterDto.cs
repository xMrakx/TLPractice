using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.ReservationDTOs;

public record ReservationFilterDto(
    [Range( 1, int.MaxValue, ErrorMessage = "ID Отклф должен быть положительным" )]
    int? PropertyId,

    [Range(1, int.MaxValue, ErrorMessage = "ID категории номера должен быть положительным")]
    int? RoomTypeId,

    [DataType( DataType.Date, ErrorMessage = "Неверный формат даты" )]
    DateTime? ArrivalDate,

    [DataType( DataType.Date, ErrorMessage = "Неверный формат даты" )]
    DateTime? DepartureDate,

    [StringLength( 30, ErrorMessage = "Имя гостя не должно превышать 30 символов" )]
    string? GuestName,

    [StringLength( 50, ErrorMessage = "Название страны не должно превышать 50 символов" )]
    string? Country,

    [StringLength( 50, ErrorMessage = "Название города не должно превышать 50 символов" )]
    string? City
);
