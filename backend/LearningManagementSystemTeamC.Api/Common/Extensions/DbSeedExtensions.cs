using LearningManagementSystemTeamC.Infrastructure.Persistence;
using LearningManagementSystemTeamC.Infrastructure.Persistence.Seeders;

namespace LearningManagementSystemTeamC.Api.Common.Extensions;

public static class DbSeedExtensions
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await RoleSeeder.SeedAsync(db);
        await UserSeeder.SeedAsync(db);
        await CourseSeeder.SeedAsync(db);
    }
}
