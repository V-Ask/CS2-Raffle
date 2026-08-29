namespace WorkshopScraper.Interfaces.Scrapers;

public interface IWorkshopExplorer
{
    string GetTitle();
    string GetImageUrl();
    string GetDescription();
    string? GetUuid();
}