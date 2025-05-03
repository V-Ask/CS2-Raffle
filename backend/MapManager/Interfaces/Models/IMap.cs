namespace MapManager.Interfaces.Models;

public interface IMap
{
    string Name { get; }
    string Description { get; }
    string Thumbnail { get; }
    string Id { get; }
    string Color { get; }
}