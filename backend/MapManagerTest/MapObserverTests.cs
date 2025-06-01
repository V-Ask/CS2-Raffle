using MapManager;
using MapManager.Interfaces.Observers;
using MapManager.Interfaces.Strategies;
using Moq;

namespace MapManagerTest;

[TestFixture]
public class MapObserverTests
{
    private class MapSubscriberStub : IMapSubscriber
    {
        public int Weight { get; private set; } = 10;

        public void IncrementWeight(int amount)
        {
            Weight = amount;
        }

        public void SetColor(string color)
        {
            if(color == "red") Weight = 99;
        }
    }

    private static readonly IMapSubscriber[] Stubs =
    [
        new MapSubscriberStub(),
        new MapSubscriberStub(),
        new MapSubscriberStub(),
        new MapSubscriberStub()
    ];

    private MapObserver _mapObserver;
    private readonly Mock<IRarityColorStrategy> _mockRarityColorStrategy = new();
    private readonly Mock<IWeightIncrementStrategy> _weightIncrementStrategy = new();

    [SetUp]
    public void Setup()
    {
        _weightIncrementStrategy
            .Setup(x => x.GetWeightIncrement(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(1);
        _mockRarityColorStrategy
            .Setup(x => x.CalculateRarityColor(It.IsAny<int>(), It.IsAny<int>()))
            .Returns("blue");
        _mapObserver = new MapObserver(_mockRarityColorStrategy.Object, _weightIncrementStrategy.Object);
    }

    [Test(Author = "VAJ", Description = "Manager should notify all subscribers and use increment strategies")]
    public void TestNotifySubscriberIncrement()
    {
        // arrange
        _weightIncrementStrategy
            .Setup(x => x.GetWeightIncrement(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(-1);
        // act
        _mapObserver.Subscribe(Stubs);
        _mapObserver.IncrementWeights();
        // assert
        var tenWeightSubs = Stubs.Count(s => s.Weight == -1);
        Assert.That(tenWeightSubs, Is.EqualTo(Stubs.Length));
    }

    [Test(Author = "VAJ", Description = "Manager should notify all subscribers and use color strategies")]
    public void TestNotifySubscriberColor()
    {
        // arrange
        _mockRarityColorStrategy
            .Setup(x => x.CalculateRarityColor(It.IsAny<int>(), It.IsAny<int>()))
            .Returns("red");
        // act
        _mapObserver.Subscribe(Stubs);
        _mapObserver.IncrementWeights();
        // assert
        var tenWeightSubs = Stubs.Count(s => s.Weight == 99);
        Assert.That(tenWeightSubs, Is.EqualTo(Stubs.Length));
    }
}