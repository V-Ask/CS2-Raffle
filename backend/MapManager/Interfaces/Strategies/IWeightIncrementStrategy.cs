namespace MapManager.Interfaces.Strategies;

public interface IWeightIncrementStrategy
{
    int GetWeightIncrement(int weight, int totalWeight);
}