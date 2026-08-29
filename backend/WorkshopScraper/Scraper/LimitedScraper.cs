using System.Threading.RateLimiting;
using WorkshopScraper.Interfaces.Scrapers;

namespace WorkshopScraper.Scraper;

public class LimitedScraper<T>(IScraper<T> scraper, int minimumScrapeIntervalMillis = 5000): IScraper<T> 
{
    private readonly RateLimiter _limiter = new TokenBucketRateLimiter(new TokenBucketRateLimiterOptions
    {
        TokenLimit = 1,
        TokensPerPeriod = 1,
        ReplenishmentPeriod = TimeSpan.FromMilliseconds(minimumScrapeIntervalMillis),
        QueueLimit = int.MaxValue,
        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
        AutoReplenishment = true
    });

    public async Task<IExplorer> LoadAsync(T dataLocation)
    {
        using var lease = await _limiter.AcquireAsync();
        if (!lease.IsAcquired)
            throw new InvalidOperationException($"Rate limit lease not acquired for '{dataLocation}'.");

        return await scraper.LoadAsync(dataLocation);
    }
}