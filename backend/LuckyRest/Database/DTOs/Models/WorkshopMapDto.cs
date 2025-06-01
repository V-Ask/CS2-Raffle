using LuckyRest.Database.Entities;

namespace LuckyRest.Database.DTOs.Models;

public class WorkshopMapDto
{
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long MapId { get; set; }

    public static WorkshopMapDto FromEntity(WorkshopMap? workshopMap)
    {
        if(workshopMap == null) return new WorkshopMapDto();
        return new WorkshopMapDto
        {
            Name = workshopMap.Name,
            ImageUrl = workshopMap.ImageUrl,
            Description = workshopMap.Description,
            MapId = workshopMap.WorkshopMapId
        };
    }
}