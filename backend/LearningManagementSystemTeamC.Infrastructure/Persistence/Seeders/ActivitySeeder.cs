using LearningManagementSystemTeamC.Domain.Activities;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Seeders;

public static class ActivitySeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Activities.Any())
        {
            return;
        }

        // Replace with actual ModuleId if needed
        var moduleId = await context.Modules.ToDictionaryAsync(m => m.ModuleName, m => m);

        var activities = new List<Activity>
        {
                new Activity(
                    "Lecture A part 1",
                    "Description for Lecture A part 1",
                new DateTime(2024, 1, 7),
                new DateTime(2024, 1, 7),
                    ActivityType.Lecture,
                    moduleId["Programing Basics"].Id
                ),
                new Activity(
                    "Lecture A part 2",
                    "Description for Lecture A part 2",
                new DateTime(2024, 1, 8),
                new DateTime(2024, 1, 8),
                    ActivityType.Lecture,
                    moduleId["Programing Basics"].Id
                ),
                new Activity(
                    "Lecture A part 3",
                    "Description for Lecture A part 3",
                new DateTime(2024, 1, 9),
                new DateTime(2024, 1, 9),
                    ActivityType.Lecture,
                    moduleId["Programing Basics"].Id
                ),
                new Activity(
                    "Lecture A part 4",
                    "Description for Lecture A part 4",
                new DateTime(2024, 1, 10),
                new DateTime(2024, 1, 10),
                    ActivityType.Lecture,
                    moduleId["Programing Basics"].Id
                ),
                new Activity(
                    "Lecture A part 5",
                    "Description for Lecture A part 5",
                new DateTime(2024, 1, 11),
                new DateTime(2024, 1, 11),
                    ActivityType.Lecture,
                    moduleId["Programing Basics"].Id
                )
            };
        context.Activities.AddRange(activities);
        await context.SaveChangesAsync();
    }
}

