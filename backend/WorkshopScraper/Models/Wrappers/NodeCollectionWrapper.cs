using HtmlAgilityPack;
using WorkshopScraper.NodeCollectionWrapper;

namespace WorkshopScraper.Models.Wrappers;

public class NodeCollectionWrapper(HtmlNodeCollection collection) : INodeCollectionWrapper
{
    public int this[HtmlNode node] => collection[node];

    public bool IsEmpty()
    {
        return collection.Count < 1;
    }
}