using LearningManagementSystemTeamC.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Seeders;

public class RoleSeeder
{
    public static async Task SeedRoles(ApplicationDbContext db)
    {
        if (await db.Roles.AnyAsync())
            return;

        var roles = new[]
        {
            new Role("Teacher", "TEACHER"),
            new Role("Student", "STUDENT"),
        };

        db.Roles.AddRange(roles);

        await db.SaveChangesAsync();
    }
}
