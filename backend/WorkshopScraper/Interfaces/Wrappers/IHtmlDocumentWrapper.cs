using HtmlAgilityPack;
using WorkshopScraper.HtmlNodeWrapper;
using WorkshopScraper.NodeCollectionWrapper;

namespace WorkshopScraper.Interfaces.Wrappers;

public interface IHtmlDocumentWrapper
{
    HtmlNode DocumentNode { get; }
    IHtmlNodeWrapper? SelectSingleDocumentNode(string selector);
    INodeCollectionWrapper SelectAllDocumentNodes(string selector);
}