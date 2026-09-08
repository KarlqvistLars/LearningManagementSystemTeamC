using LearningManagementSystemTeamC.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Seeders;

public class RoleSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        if (await db.Roles.AnyAsync())
            return;

        var roles = new[]
        {
            new Role(RoleRules.TeacherRoleName, RoleRules.TeacherRoleCode),
            new Role(RoleRules.StudentRoleName, RoleRules.StudentRoleCode),
        };

        db.Roles.AddRange(roles);

        await db.SaveChangesAsync();
    }
}
