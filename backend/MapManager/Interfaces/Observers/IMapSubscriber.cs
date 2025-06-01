namespace MapManager.Interfaces.Observers;

public interface IMapSubscriber
{
    int Weight { get; }
    void IncrementWeight(int amount);
    void SetColor(string color);
}