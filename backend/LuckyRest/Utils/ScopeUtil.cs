using LuckyRest.Database.DAOs.UserDao;
using LuckyRest.Database.DAOs.WorkshopMapDao;
using LuckyRest.Database.DAOs.WorkshopPlaylistDao;
using LuckyRest.Database.DAOs.WorkshopPlaylistMapDao;
using LuckyRest.Services.UserService;
using LuckyRest.Services.WorkshopMapService;
using LuckyRest.Services.WorkshopPlaylistService;

namespace LuckyRest.Utils;

public static class ScopeUtil
{
    public static void AddScopes(this IHostApplicationBuilder builder)
    {
        AddServiceScope(builder);
        AddDaoScope(builder);
    }
    private static void AddServiceScope(IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IWorkshopMapService, WorkshopMapService>();
        builder.Services.AddScoped<IWorkshopPlaylistService, WorkshopPlaylistService>();
    }

    private static void AddDaoScope(IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserDao, UserDao>();
        builder.Services.AddScoped<IWorkshopMapDao, WorkshopMapDao>();
        builder.Services.AddScoped<IWorkshopPlaylistDao, WorkshopPlaylistDao>();
        builder.Services.AddScoped<IWorkshopPlaylistMapDao, WorkshopPlaylistMapDao>();
    }
}