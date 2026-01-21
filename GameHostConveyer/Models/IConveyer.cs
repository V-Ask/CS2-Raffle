namespace GameHostConveyer.Models;

public interface IConveyer : IDisposable
{
    Task<bool> StartServer();
    Task<bool> StopServer();
}