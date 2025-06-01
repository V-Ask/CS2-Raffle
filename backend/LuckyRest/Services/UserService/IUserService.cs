using LuckyRest.Database.Entities;
using LuckyRest.Utils;

namespace LuckyRest.Services.UserService;

public interface IUserService
{
    public Task<ServiceResult> SetUserRestEndpoint(User user, string endpoint);
}