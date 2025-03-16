namespace WorkshopScraper.HtmlNodeWrapper;

public interface IHtmlNodeWrapper
{
    string InnerText { get; }
    string? ImageSource { get; }
}