using Moq;
using WorkshopScraper.HtmlNodeWrapper;
using WorkshopScraper.Interfaces.Scrapers;
using WorkshopScraper.Scraper;

namespace WorkshopScraperTest.Scrapers;

public class SteamWorkshopExplorerTest
{
    private SteamWorkshopExplorer _steamWorkshopExplorer;
    private readonly Mock<IExplorer> _mockScraper = new();
    private readonly Mock<IHtmlNodeWrapper> _mockHtmlNodeWrapper = new();
    
    [SetUp]
    public void Setup()
    {
        _mockHtmlNodeWrapper.Setup(x => x.InnerText).Returns("Test InnerText");
        _mockHtmlNodeWrapper.Setup(x => x.ImageSource).Returns("Test ImageSource");
        _steamWorkshopExplorer = new SteamWorkshopExplorer(_mockScraper.Object);
    }

    [Test(Author = "VAJ", Description = "WebScraper can get title of workshop map")]
    public void TestGetTitle()
    {
        // arrange
        _mockScraper.Setup(x => x.GetSingleElement("//div[contains(@class, 'workshopItemTitle')]")).Returns(_mockHtmlNodeWrapper.Object);
        // act
        var result = _steamWorkshopExplorer.GetTitle();
        // assert
        Assert.That(result, Is.EqualTo("Test InnerText"));
    }
    
    
    [Test(Author = "VAJ", Description = "WebScraper can get description of workshop map")]
    public void TestGetDescription()
    {
        // arrange
        _mockScraper.Setup(x => x.GetSingleElement("//div[contains(@class, 'workshopItemDescription')]")).Returns(_mockHtmlNodeWrapper.Object);
        // act
        var result = _steamWorkshopExplorer.GetDescription();
        // assert
        Assert.That(result, Is.EqualTo("Test InnerText"));
    }
    
    [Test(Author = "VAJ", Description = "WebScraper can get image url")]
    public void TestGetImageUrl()
    {
        // arrange
        _mockScraper.Setup(x => x.GetSingleElement("//img[contains(@class, 'workshopItemPreviewImageMain')]")).Returns(_mockHtmlNodeWrapper.Object);
        // act
        var result = _steamWorkshopExplorer.GetImageUrl();
        // assert
        Assert.That(result, Is.EqualTo("Test ImageSource"));
    }
    
    [Test(Author = "VAJ", Description = "WebScraper can get id of workshop map")]
    public void TestGetId()
    {
        // arrange
        _mockScraper.Setup(x => x.GetQueryValue("id")).Returns("Test Id");
        // act
        var result = _steamWorkshopExplorer.GetUuid();
        // assert
        Assert.That(result, Is.EqualTo("Test Id"));
    }
    
    [Test(Author = "VAJ", Description = "Workshop Urls get properly recognized")]
    [TestCase(@"https://steamcommunity.com/workshop/filedetails/?id=1111111111")]
    [TestCase(@"steamcommunity.com/workshop/filedetails/?id=0000000000")]
    public void PositiveTestIsWorkshopUrl(string url)
    {
        // act
        var result = SteamWorkshopExplorer.IsWorkshopUrl(url);
        // assert
        Assert.That(result, Is.True);
    }
    
    [Test(Author = "VAJ", Description = "non-Workshop Urls do not get recognized")]
    [TestCase(@"this is not a url")]
    [TestCase(@"www.thisisnotvalid.url")]
    [TestCase("")]
    [TestCase("https://steamcommunity.com/workshop/filedetails/?id=1")]
    public void NegativeTestIsWorkshop(string url)
    {
        // act
        var result = SteamWorkshopExplorer.IsWorkshopUrl(url);
        // assert
        Assert.That(result, Is.False);
    }
}