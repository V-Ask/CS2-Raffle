namespace LuckyRest.Database.DTOs.Models;

public class SpawnCountDto
{
    public long WorkshopId { get; set; }
    public int? TerroristSpawns { get; set; }
    public int? CounterTerroristSpawns { get; set; }
}
