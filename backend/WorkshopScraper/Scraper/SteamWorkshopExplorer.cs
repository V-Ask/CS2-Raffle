using System.Collections.Specialized;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using WorkshopScraper.HtmlNodeWrapper;
using WorkshopScraper.Interfaces.Scrapers;

namespace WorkshopScraper.Scraper;

public partial class SteamWorkshopExplorer(IExplorer explorer) : IWorkshopExplorer
{
    private const string WorkshopLinkPattern = @"^(https://)?steamcommunity\.com/workshop/filedetails/\?id=(\d{10})";

    private string GetPreviewImage =>
        explorer.GetSingleElement("//img[contains(@id, 'previewImageMain')]")
            .ImageSource;

    private string GetEnlargedPreviewImage =>
        explorer.GetSingleElement("//img[contains(@class, 'workshopItemPreviewImageEnlargeable')]")
            .ImageSource;

    public string GetTitle()
    {
        return explorer.GetSingleElement("//div[contains(@class, 'workshopItemTitle')]").InnerText;
    }

    public string GetImageUrl()
    {
        var previewImage = GetPreviewImage;
        return previewImage != "" ? previewImage : GetEnlargedPreviewImage;
    }


    public string GetDescription()
    {
        return explorer.GetSingleElement("//div[contains(@class, 'workshopItemDescription')]").InnerText;
    }

    public string? GetUuid()
    {
        return explorer.GetQueryValue("id");
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