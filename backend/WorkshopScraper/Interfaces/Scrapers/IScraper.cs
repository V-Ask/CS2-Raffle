namespace WorkshopScraper.Interfaces.Scrapers;

public interface IScraper<in T>
{
    public Task<IExplorer> LoadAsync(T dataLocation);
}