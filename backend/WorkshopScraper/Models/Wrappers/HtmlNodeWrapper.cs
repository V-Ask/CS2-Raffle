using HtmlAgilityPack;
using WorkshopScraper.HtmlNodeWrapper;

namespace WorkshopScraper.Models.Wrappers;

public class HtmlNodeWrapper(HtmlNode? node) : IHtmlNodeWrapper
{
    public string InnerText { get; } = node?.InnerText ?? string.Empty;

    public string ImageSource => node != null && HasImageSource() ? node.Attributes["src"].Value : string.Empty;

    private bool HasImageSource()
    {
        return node != null && node.Attributes.Contains("src");
    }
}