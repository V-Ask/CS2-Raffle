using System.Net.Http.Headers;
using System.Text;

namespace GameHostConveyer.Models;

public class DatHostConveyer : IConveyer
{
    private readonly HttpClient _httpClient = new();

    public DatHostConveyer(string serverId, string email, string password)
    {
        var baseAddress = new Uri($"https://dathost.net/api/0.1/game-servers/{serverId}/");
        var authString = $"{email}:{password}";
        var base64EncodedAuthString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authString));
        _httpClient.BaseAddress = baseAddress;
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", base64EncodedAuthString);
    }

    public async Task<bool> StartServer()
    {
        var formData = new Dictionary<string, string>()
        {
            { "allow_host_reassignment", "false" }
        };
        var content = new FormUrlEncodedContent(formData);
        var result = await _httpClient.PostAsync("/start", content);
        return result.IsSuccessStatusCode;
    }

    public async Task<bool> StopServer()
    {
        var result = await _httpClient.PostAsync("/stop", null);
        return result.IsSuccessStatusCode;
    }

    public async Task<bool> SelectWorkshopMap(long workshopId)
    {
        var formData = new Dictionary<string, string>
        {
            { "cs2_settings.maps_source", "workshop_single_map" },
            { "cs2_settings.workshop_single_map_id", workshopId.ToString() }
        };
        var content = new FormUrlEncodedContent(formData);
        var result = await _httpClient.PutAsync("", content);
        return result.IsSuccessStatusCode;
    }

    public async Task<bool> TestConnection()
    {
        var result = await _httpClient.GetAsync("");
        return result.IsSuccessStatusCode;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        GC.SuppressFinalize(this);
    }
}