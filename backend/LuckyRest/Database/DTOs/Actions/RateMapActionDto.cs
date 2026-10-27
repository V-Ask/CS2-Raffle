namespace LuckyRest.Database.DTOs.Actions;

public class RateMapActionDto
{
    public Guid PlaylistId { get; set; }
    public long MapId { get; set; }
    public int Rating { get; set; }
}