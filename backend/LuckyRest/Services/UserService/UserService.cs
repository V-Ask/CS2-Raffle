using LuckyRest.Database.DAOs.UserDao;
using LuckyRest.Database.DTOs.Models;
using LuckyRest.Database.Entities;
using LuckyRest.Utils;

namespace LuckyRest.Services.UserService;

public class UserService() : IUserService
{
    public ServiceResult<AuthUserDto> AuthUser(User user)
    {
        return ServiceResult.Success.WithData(new AuthUserDto
        {
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
        });
    }
}