namespace MapManager.Interfaces.Strategies;

public interface IRarityColorStrategy
{
    string CalculateRarityColor(int currentWeight, int totalWeight);
}