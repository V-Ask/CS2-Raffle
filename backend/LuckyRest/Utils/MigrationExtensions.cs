using LuckyRest.Database;
using Microsoft.EntityFrameworkCore;

namespace LuckyRest.Utils;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<LuckyDbContext>();
        context.Database.Migrate();
    }
}