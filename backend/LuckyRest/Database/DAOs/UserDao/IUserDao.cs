namespace LuckyRest.Database.DAOs.UserDao;

public interface IUserDao
{
    public Task SetUserEndpoint(Entities.User user, string endpoint);
}