using WorkshopScraper.HtmlNodeWrapper;
using WorkshopScraper.NodeCollectionWrapper;

namespace WorkshopScraper.Interfaces.Scrapers;

public interface IExplorer
{
    string Url { get; }
    string? GetQueryValue(string query);
    IHtmlNodeWrapper GetSingleElement(string xpathSelector);
    INodeCollectionWrapper GetAllElements(string xpathSelector);
}