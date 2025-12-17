using LuckyRest.Database.DAOs.UserDao;
using LuckyRest.Database.Entities;
using LuckyRest.Utils;

namespace LuckyRest.Services.UserService;

public class UserService(IUserDao userDao) : IUserService
{
}