using MapManager.Interfaces.Models;
using MapManager.Interfaces.Observers;

namespace MapManager.Models;

public class Map(string name, string description, string thumbnail, string id)
    : IMap, IMapSubscriber
{
    public string Name { get; } = name;
    public string Description { get; } = description;
    public string Thumbnail { get; } = thumbnail;
    public string Id { get; } = id;
    public string Color { get; private set; } = "FFFFFF";
    public int Weight { get; private set; } = 1;

    public void IncrementWeight(int amount)
    {
        Weight += amount;
    }

    public void SetColor(string color)
    {
        Color = color;
    }
}