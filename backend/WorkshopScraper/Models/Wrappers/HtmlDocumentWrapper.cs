using HtmlAgilityPack;
using WorkshopScraper.HtmlNodeWrapper;
using WorkshopScraper.Interfaces.Wrappers;
using WorkshopScraper.NodeCollectionWrapper;

namespace WorkshopScraper.Models.Wrappers;

public class HtmlDocumentWrapper(HtmlDocument htmlDocument) : IHtmlDocumentWrapper
{
    public HtmlNode DocumentNode => htmlDocument.DocumentNode;
    
    public IHtmlNodeWrapper? SelectSingleDocumentNode(string selector)
    {
        var node = DocumentNode.SelectSingleNode(selector);
        return new HtmlNodeWrapper(node);
    }

    public INodeCollectionWrapper SelectAllDocumentNodes(string selector)
    {
        return new NodeCollectionWrapper(DocumentNode.SelectNodes(selector));
    }
}