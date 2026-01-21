namespace WorkshopScraper.Interfaces.Scrapers;

public interface IWorkshopScraper
{
    string GetTitle();
    string GetImageUrl();
    string GetDescription();
    string? GetUuid();
}