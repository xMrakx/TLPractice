using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.ReservationDTOs;

public record ReservationAddDto(
    [Required( ErrorMessage = "ID отеля обязателен" )]
    [Range( 1, int.MaxValue, ErrorMessage = "ID отеля должен быть положительным числом" )]
    int PropertyId,

    [Required( ErrorMessage = "ID типа номера обязателен" )]
    [Range( 1, int.MaxValue, ErrorMessage = "ID типа номера должен быть положительным числом" )]
    int RoomTypeId,

    [Required( ErrorMessage = "Дата заезда обязательна" )]
    [DataType( DataType.Date, ErrorMessage = "Неверный формат даты" )]
    DateTime ArrivalDate,

    [Required( ErrorMessage = "Дата выезда обязательна" )]
    [DataType( DataType.Date, ErrorMessage = "Неверный формат даты" )]
    DateTime DepartureDate,

    [Required( ErrorMessage = "Имя гостя обязательно" )]
    [StringLength( 30, ErrorMessage = "Имя гостя не должно превышать 30 символов" )]
    string GuestName,

    [Required( ErrorMessage = "Номер телефона гостя обязателен" )]
    [StringLength( 15, ErrorMessage = "Номер телефона гостя не должен превышать 15 символов" )]
    string GuestPhoneNumber,

    [Required( ErrorMessage = "Количество гостей обязательно" )]
    [Range( 1, int.MaxValue, ErrorMessage = "Количество гостей должно быть положительным числом" )]
    int GuestCount
);
