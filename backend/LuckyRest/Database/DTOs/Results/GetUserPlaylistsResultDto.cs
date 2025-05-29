namespace LuckyRest.Database.DTOs.Results;

public class GetUserPlaylistsResultDto
{
    public IList<WorkshopPlaylistDto> WorkshopPlaylists { get; set; } =  new List<WorkshopPlaylistDto>();
}