using LearningManagementSystemTeamC.Domain.Roles;
using LearningManagementSystemTeamC.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Seeders;

public class UserSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        if (await db.Users.AnyAsync())
            return;

        var defaultRole = await db.Roles.FirstAsync(r => r.Code == RoleRules.DefaultRoleCode);
        var teacherRole = await db.Roles.FirstAsync(r => r.Code == RoleRules.TeacherRoleCode);

        var password = "pass";
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var inactiveDefaultUser = new User("inactive@lms.com", hashedPassword, defaultRole.Id);
        var activeDefaultUser = new User("student@lms.com", hashedPassword, defaultRole.Id);
        var activeTeacherUser = new User("admin@lms.com", hashedPassword, teacherRole.Id);

        inactiveDefaultUser.Disable();

        var users = new[] { activeTeacherUser, inactiveDefaultUser, activeDefaultUser };

        db.Users.AddRange(users);
        await db.SaveChangesAsync();
    }

}
