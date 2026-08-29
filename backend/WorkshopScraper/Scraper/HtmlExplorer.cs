using System.Collections.Specialized;
using System.Web;
using WorkshopScraper.HtmlNodeWrapper;
using WorkshopScraper.Interfaces.Scrapers;
using WorkshopScraper.Interfaces.Wrappers;
using WorkshopScraper.Models.Wrappers;
using WorkshopScraper.NodeCollectionWrapper;

namespace WorkshopScraper.Scraper;

using HtmlAgilityPack;

public class HtmlExplorer(IHtmlDocumentWrapper htmlDocument, NameValueCollection queries, string url) : IExplorer
{
    public string Url => url;
    public string? GetQueryValue(string query) => queries[query];

    public IHtmlNodeWrapper GetSingleElement(string xpathSelector)
    {
        return htmlDocument.SelectSingleDocumentNode(xpathSelector);
    }
    
    public INodeCollectionWrapper GetAllElements(string xpathSelector)
    {
        return htmlDocument.SelectAllDocumentNodes(xpathSelector);
    }
}