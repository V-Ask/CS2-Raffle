using System.Collections.Specialized;
using HtmlAgilityPack;
using Moq;
using WorkshopScraper.HtmlNodeWrapper;
using WorkshopScraper.Interfaces.Wrappers;
using WorkshopScraper.NodeCollectionWrapper;
using WorkshopScraper.Scraper;

namespace WorkshopScraperTest.Scrapers;

[TestFixture]
public class GenericScraperTest
{
    
    private readonly Mock<IHtmlDocumentWrapper> _htmlDocumentWrapperMock = new();
    private const string Url = "https://thisisaurl.com";
    private GenericScraper _scraper;
    
    [SetUp]
    public void Setup()
    {
        var query = new NameValueCollection
        {
            ["a"] = "1",
            ["b"] = "2",
            ["c"] = "3"
        };
        _scraper = new GenericScraper(_htmlDocumentWrapperMock.Object, query, Url);
    }

    [Test(Description = "Query values should have the correct index and value")]
    public void TestQuerySelector()
    {
        // act
        var elementA = _scraper.GetQueryValue("a");
        var elementB = _scraper.GetQueryValue("b");
        var elementC = _scraper.GetQueryValue("c");
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(elementA, Is.EqualTo("1"));
            Assert.That(elementB, Is.EqualTo("2"));
            Assert.That(elementC, Is.EqualTo("3"));
        });
    }

    [Test(Author = "VAJ", Description = "Should return matching HtmlNode from xpath")]
    public void TestGetSingleElement()
    {
        // arrange
        var singleElementMock = new Mock<IHtmlNodeWrapper>();
        _htmlDocumentWrapperMock.Setup(x => x.SelectSingleDocumentNode(It.IsAny<string>())).Returns<HtmlNode>(null!);
        _htmlDocumentWrapperMock.Setup(x => x.SelectSingleDocumentNode("test")).Returns(singleElementMock.Object);
        // act
        var element = _scraper.GetSingleElement("test");
        var nullElement = _scraper.GetSingleElement("anything else");
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(element, Is.SameAs(singleElementMock.Object));
            Assert.That(nullElement, Is.Null);
        });
    }

    [Test(Author = "VAJ", Description = "Should return matching HtmlNodes from xpath")]
    public void TestGetAllElements()
    {
        // arrange
        var nodeCollectionMock = new Mock<INodeCollectionWrapper>();
        var emptyCollectionMock = new Mock<INodeCollectionWrapper>();
        nodeCollectionMock.Setup(x => x.IsEmpty()).Returns(false);
        emptyCollectionMock.Setup(x => x.IsEmpty()).Returns(true);
        _htmlDocumentWrapperMock.Setup(x => x.SelectAllDocumentNodes(It.IsAny<string>())).Returns(emptyCollectionMock.Object);
        _htmlDocumentWrapperMock.Setup(x => x.SelectAllDocumentNodes("test")).Returns(nodeCollectionMock.Object);
        // act
        var nodeCollection = _scraper.GetAllElements("test");
        var emptyCollection = _scraper.GetAllElements("anything else");
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(nodeCollection.IsEmpty(), Is.False);
            Assert.That(emptyCollection.IsEmpty(), Is.True);
        });
    }

    [Test(Author = "VAJ", Description = "Should return correct Url")]
    public void TestGetUrl()
    {
        // assert
        Assert.That(_scraper.Url, Is.EqualTo(Url));
    }
}