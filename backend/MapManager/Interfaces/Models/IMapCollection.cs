namespace MapManager.Interfaces.Models;

public interface IMapCollection
{
    int[] MapIds { get; }
    void AddMap(IMap map);
    void RemoveMap(IMap map);
}