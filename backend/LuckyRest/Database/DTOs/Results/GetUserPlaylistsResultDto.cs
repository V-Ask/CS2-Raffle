using LuckyRest.Database.DTOs.Models;

namespace LuckyRest.Database.DTOs.Results;

public class GetUserPlaylistsResultDto
{
    public IList<WorkshopPlaylistIndexDto> WorkshopPlaylists { get; set; } =  new List<WorkshopPlaylistIndexDto>();
}