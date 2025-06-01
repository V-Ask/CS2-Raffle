using HtmlAgilityPack;
using WorkshopScraper.HtmlNodeWrapper;

namespace WorkshopScraper.Models.Wrappers;

public class HtmlNodeWrapper(HtmlNode node) : IHtmlNodeWrapper
{
    public string InnerText { get; } = node.InnerText;

    public string? ImageSource => node.Attributes.Contains("src") ? node.Attributes["src"].Value : null;
}