using MapManager.Interfaces.Models;
using MapManager.Models;

namespace MapManagerTest;

public class MapTests
{
    private const string Name = "TestMap";
    private const string Description = "TestMapDescription";
    private const string Thumbnail = "TestMapThumbnail";
    private const string Id = "TestId";

    private Map _map;
    
    [SetUp]
    public void Setup()
    {
        _map = new Map(Name, Description, Thumbnail, Id);
    }

    [Test(Author = "VAJ", Description = "Test for map name")]
    public void TestGetName()
    {
        Assert.That(_map.Name, Is.EqualTo(Name));
    }

    [Test(Author = "VAJ", Description = "Test for map description")]
    public void TestGetDescription()
    {
        Assert.That(_map.Description, Is.EqualTo(Description));
    }

    [Test(Author = "VAJ", Description = "Test for map thumbnail")]
    public void TestGetThumbnail()
    {
        Assert.That(_map.Thumbnail, Is.EqualTo(Thumbnail));
    }

    [Test(Author = "VAJ", Description = "Test for map id")]
    public void TestGetId()
    {
        Assert.That(_map.Id, Is.EqualTo(Id));
    }
    
    [Test(Author = "VAJ", Description = "Test initial color is white")]
    public void TestGetInitialColor()
    {
        Assert.That(_map.Color, Is.EqualTo("FFFFFF"));
    }
    
    [Test(Author = "VAJ", Description = "Test initial weight is 1")]
    public void TestGetInitialWeight()
    {
        Assert.That(_map.Weight, Is.EqualTo(1));
    }
    
    [Test(Author = "VAJ", Description = "Test increment weight")]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(int.MaxValue - 1)]
    public void TestIncrementWeight(int increment)
    {
        // act
        _map.IncrementWeight(increment);
        // assert
        Assert.That(_map.Weight, Is.EqualTo(increment + 1));
    }
    
    [Test(Author = "VAJ", Description = "Test increment weight")]
    [TestCase("FF0000")]
    [TestCase("1100FF")]
    public void TestSetColor(string color)
    {
        // act
        _map.SetColor(color);
        // assert
        Assert.That(_map.Color, Is.EqualTo(color));
    }
    
}