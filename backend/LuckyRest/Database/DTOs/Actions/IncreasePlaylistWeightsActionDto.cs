namespace LuckyRest.Database.DTOs.Actions;

public class IncreasePlaylistWeightsActionDto
{
    public Guid PlaylistId { get; set; }
    public int Increment { get; set; }
    public long[] Exceptions { get; set; }
    public bool RemoveExceptions { get; set; }
}