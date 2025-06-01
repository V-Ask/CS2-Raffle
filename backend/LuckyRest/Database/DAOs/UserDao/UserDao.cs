using LuckyRest.Database.Entities;

namespace LuckyRest.Database.DAOs.UserDao;

public class UserDao(LuckyDbContext dbContext) : IUserDao
{
    public async Task SetUserEndpoint(User user, string endpoint)
    {
        user.LaunchServerEndpoint = endpoint;
        await dbContext.SaveChangesAsync();
        return;
    }
}