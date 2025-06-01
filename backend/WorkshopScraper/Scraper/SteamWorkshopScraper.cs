using System.Collections.Specialized;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using WorkshopScraper.HtmlNodeWrapper;
using WorkshopScraper.Interfaces.Scrapers;

namespace WorkshopScraper.Scraper;

public partial class SteamWorkshopScraper(IScraper scraper) : IWorkshopScraper
{
    private const string WorkshopLinkPattern = @"^(https://)?steamcommunity\.com/workshop/filedetails/\?id=(\d{10})";

    public string? GetTitle()
    {
        return scraper.GetSingleElement("//div[contains(@class, 'workshopItemTitle')]")?.InnerText;
    }

    public string? GetImageUrl()
    {
        return scraper.GetSingleElement("//img[contains(@class, 'workshopItemPreviewImageMain')]")?.ImageSource;
    }

    public string? GetDescription()
    {
        return scraper.GetSingleElement("//div[contains(@class, 'workshopItemDescription')]")?.InnerText;
    }

    public string? GetUuid()
    {
        return scraper.GetQueryValue("id");
    }

    public static bool IsWorkshopUrl(string url)
    {
        return WorkshopRegex().IsMatch(url);
    }

    [GeneratedRegex(WorkshopLinkPattern)]
    private static partial Regex WorkshopRegex();
}

public class LinkIsNotWorkshopException : Exception
{
}