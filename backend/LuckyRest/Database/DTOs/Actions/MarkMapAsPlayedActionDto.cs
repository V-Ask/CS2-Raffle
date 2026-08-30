namespace LuckyRest.Database.DTOs.Actions;

public record MarkMapAsPlayedActionDto(
    Guid PlaylistId,
    long MapId,
    bool Played
);