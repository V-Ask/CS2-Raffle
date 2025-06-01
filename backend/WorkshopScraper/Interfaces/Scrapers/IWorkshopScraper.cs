using WorkshopScraper.Scraper;

namespace WorkshopScraper.Scraper;

public interface IWorkshopScraper
{
    string? GetTitle();
    string? GetImageUrl();
    string? GetDescription();
    string? GetUuid();
}