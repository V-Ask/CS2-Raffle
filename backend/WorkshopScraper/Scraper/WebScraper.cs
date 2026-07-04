using System.Web;
using HtmlAgilityPack;
using WorkshopScraper.Interfaces.Scrapers;
using WorkshopScraper.Models.Wrappers;

namespace WorkshopScraper.Scraper;

public class WebScraper : IScraper<string>
{
    private readonly HtmlWeb _web = new();
    
    public async Task<IExplorer> LoadAsync(string dataLocation)
    {
        var document = await _web.LoadFromWebAsync(dataLocation);
        var documentWrapper = new HtmlDocumentWrapper(document);
        var uri = new Uri(dataLocation);
        var queries = HttpUtility.ParseQueryString(uri.Query);
        return new HtmlExplorer(documentWrapper, queries, dataLocation);
    }
}