using LuckyRest.Database.DTOs.Models;

namespace LuckyRest.Database.DTOs.Results;

public class SpawnBatchResultDto
{
    public IList<SpawnCountDto> Results { get; set; } = new List<SpawnCountDto>();
}
