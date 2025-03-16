using HtmlAgilityPack;

namespace WorkshopScraper.NodeCollectionWrapper;

public interface INodeCollectionWrapper
{
    int this[HtmlNode node] { get; }
    bool IsEmpty();
}