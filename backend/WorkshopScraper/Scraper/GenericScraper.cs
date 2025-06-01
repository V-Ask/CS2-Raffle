using System.Collections.Specialized;
using System.Web;
using WorkshopScraper.HtmlNodeWrapper;
using WorkshopScraper.Interfaces.Scrapers;
using WorkshopScraper.Interfaces.Wrappers;
using WorkshopScraper.Models.Wrappers;
using WorkshopScraper.NodeCollectionWrapper;

namespace WorkshopScraper.Scraper;

using HtmlAgilityPack;

public class GenericScraper(IHtmlDocumentWrapper htmlDocument, NameValueCollection queries, string url) : IScraper
{
    public string Url => url;
    public string? GetQueryValue(string query) => queries[query];

    public IHtmlNodeWrapper? GetSingleElement(string xpathSelector)
    {
        return htmlDocument.SelectSingleDocumentNode(xpathSelector);
    }
    
    public INodeCollectionWrapper GetAllElements(string xpathSelector)
    {
        return htmlDocument.SelectAllDocumentNodes(xpathSelector);
    }

    public static GenericScraper Load(string url)
    {
        var document = new HtmlDocumentWrapper(new HtmlWeb().Load(url));
        var uri = new Uri(url);
        var queries = HttpUtility.ParseQueryString(uri.Query);
        return new GenericScraper(document, queries, url);
    }
}