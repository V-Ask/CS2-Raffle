using MapManager.Interfaces.Models;

namespace MapManager.Models;

public class MapCollection(int[] mapIds) : IMapCollection
{
    public int[] MapIds { get; } = mapIds;

    public void AddMap(IMap map)
    {
        throw new NotImplementedException();
    }

    public void RemoveMap(IMap map)
    {
        throw new NotImplementedException();
    }
}