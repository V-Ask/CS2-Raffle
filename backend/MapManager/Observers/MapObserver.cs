using MapManager.Interfaces.Observers;
using MapManager.Interfaces.Strategies;

namespace MapManager;

public class MapObserver(IRarityColorStrategy rarityColorStrategy, IWeightIncrementStrategy weightIncrementStrategy)
    : IMapObserver
{
    private readonly List<IMapSubscriber> _listeners = [];

    public void Subscribe(params IMapSubscriber[] listeners)
    {
        _listeners.AddRange(listeners);
    }

    public void Unsubscribe(IMapSubscriber subscriber)
    {
        throw new NotImplementedException();
    }

    public void IncrementWeights()
    {
        var totalWeight = _listeners.Sum(l => l.Weight);
        var newTotal = totalWeight;
        
        _listeners.ForEach(l =>
        {
            var weightIncrement = weightIncrementStrategy.GetWeightIncrement(l.Weight, totalWeight);
            newTotal += weightIncrement;
            l.IncrementWeight(weightIncrement);
        });
        
        _listeners.ForEach(l =>
        {
            var newColor = rarityColorStrategy.CalculateRarityColor(l.Weight, newTotal);
            l.SetColor(newColor);
        });
    }
}