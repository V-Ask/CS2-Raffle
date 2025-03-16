using Moq;
using WorkshopScraper.HtmlNodeWrapper;
using WorkshopScraper.Interfaces.Scrapers;
using WorkshopScraper.Scraper;

namespace WorkshopScraperTest.Scrapers;

public class SteamWorkshopScraperTest
{
    private SteamWorkshopScraper _steamWorkshopScraper;
    private readonly Mock<IScraper> _mockScraper = new();
    private readonly Mock<IHtmlNodeWrapper> _mockHtmlNodeWrapper = new();
    
    [SetUp]
    public void Setup()
    {
        _mockHtmlNodeWrapper.Setup(x => x.InnerText).Returns("Test InnerText");
        _mockHtmlNodeWrapper.Setup(x => x.ImageSource).Returns("Test ImageSource");
        _steamWorkshopScraper = new SteamWorkshopScraper(_mockScraper.Object);
    }

    [Test(Author = "VAJ", Description = "Scraper can get title of workshop map")]
    public void TestGetTitle()
    {
        // arrange
        _mockScraper.Setup(x => x.GetSingleElement(".workshopTitle")).Returns(_mockHtmlNodeWrapper.Object);
        // act
        var result = _steamWorkshopScraper.GetTitle();
        // assert
        Assert.That(result, Is.EqualTo("Test InnerText"));
    }
    
    
    [Test(Author = "VAJ", Description = "Scraper can get description of workshop map")]
    public void TestGetDescription()
    {
        // arrange
        _mockScraper.Setup(x => x.GetSingleElement(".workshopItemDescription")).Returns(_mockHtmlNodeWrapper.Object);
        // act
        var result = _steamWorkshopScraper.GetDescription();
        // assert
        Assert.That(result, Is.EqualTo("Test InnerText"));
    }
    
    [Test(Author = "VAJ", Description = "Scraper can get image url")]
    public void TestGetImageUrl()
    {
        // arrange
        _mockScraper.Setup(x => x.GetSingleElement(".workshopItemPreviewImageMain")).Returns(_mockHtmlNodeWrapper.Object);
        // act
        var result = _steamWorkshopScraper.GetImageUrl();
        // assert
        Assert.That(result, Is.EqualTo("Test ImageSource"));
    }
    
    [Test(Author = "VAJ", Description = "Scraper can get id of workshop map")]
    public void TestGetId()
    {
        // arrange
        _mockScraper.Setup(x => x.GetQueryValue("id")).Returns("Test Id");
        // act
        var result = _steamWorkshopScraper.GetUuid();
        // assert
        Assert.That(result, Is.EqualTo("Test Id"));
    }
    
    [Test(Author = "VAJ", Description = "Workshop Urls get properly recognized")]
    [TestCase(@"https://steamcommunity.com/workshop/filedetails/?id=1111111111")]
    [TestCase(@"steamcommunity.com/workshop/filedetails/?id=0000000000")]
    public void PositiveTestIsWorkshopUrl(string url)
    {
        // act
        var result = SteamWorkshopScraper.IsWorkshopUrl(url);
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
        var result = SteamWorkshopScraper.IsWorkshopUrl(url);
        // assert
        Assert.That(result, Is.False);
    }
}