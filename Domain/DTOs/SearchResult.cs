using Domain.Entities;

namespace Domain.DTOs;

public record SearchResult(
    Property Property,
    RoomType RoomType
);
