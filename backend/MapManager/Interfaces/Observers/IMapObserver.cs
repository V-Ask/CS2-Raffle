namespace MapManager.Interfaces.Observers;

public interface IMapObserver
{
    void Subscribe(params IMapSubscriber[] listeners);
    void Unsubscribe(IMapSubscriber subscriber);
    void IncrementWeights();
}