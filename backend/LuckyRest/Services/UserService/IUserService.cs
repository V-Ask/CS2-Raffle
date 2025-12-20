using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.Entities;
using LuckyRest.Utils;

namespace LuckyRest.Services.UserService;

public interface IUserService
{
    ServiceResult<AuthUserDto> AuthUser(User user);
}