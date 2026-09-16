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
                ),
                new Activity(
                    "Programing Basics Assigment 1",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                new DateTime(2024, 1, 12),
                new DateTime(2026, 3, 5),
                    ActivityType.Assignment,
                    moduleId["Programing Basics"].Id
                ),
                new Activity(
                    "Programing Basics Assigment 2",
                    "Lorem ipsum dolor sit amet consectetur adipiscing elit. Quisque faucibus ex sapien vitae pellentesque sem placerat.",
                new DateTime(2024, 1, 13),
                new DateTime(2026, 4, 1),
                    ActivityType.Assignment,
                    moduleId["Programing Basics"].Id
                ),
                new Activity(
                    "Programing Basics Assigment 3",
                    "Lorem ipsum dolor sit amet consectetur adipiscing elit. Quisque faucibus ex sapien vitae pellentesque sem placerat. In id cursus mi pretium tellus duis convallis. Tempus leo eu aenean sed diam urna tempor. Pulvinar vivamus fringilla lacus nec metus bibendum egestas. Iaculis massa nisl malesuada lacinia integer nunc posuere. Ut hendrerit semper vel class aptent taciti sociosqu. Ad litora torquent per conubia nostra inceptos himenaeos.",
                new DateTime(2024, 1, 14),
                new DateTime(2026, 5, 7),
                    ActivityType.Assignment,
                    moduleId["Programing Basics"].Id
                ),
                new Activity(
                    "Programing Basics Assigment 4",
                    "Lorem ipsum dolor sit amet consectetur adipiscing elit. Quisque faucibus ex sapien vitae pellentesque sem placerat. In id cursus mi pretium tellus duis convallis. Tempus leo eu aenean sed diam urna tempor. Pulvinar vivamus fringilla lacus nec metus bibendum egestas. Iaculis massa nisl malesuada lacinia integer nunc posuere. Ut hendrerit semper vel class aptent taciti sociosqu. Ad litora torquent per conubia nostra inceptos himenaeos.",
                new DateTime(2024, 1, 14),
                new DateTime(2026, 10, 10),
                    ActivityType.Assignment,
                    moduleId["Programing Basics"].Id
                ),
                new Activity(
                    "Programing Basics Assigment 5",
                    "Lorem ipsum dolor sit amet consectetur adipiscing elit. Quisque faucibus ex sapien vitae pellentesque sem placerat. In id cursus mi pretium tellus duis convallis. Tempus leo eu aenean sed diam urna tempor. Pulvinar vivamus fringilla lacus nec metus bibendum egestas. Iaculis massa nisl malesuada lacinia integer nunc posuere. Ut hendrerit semper vel class aptent taciti sociosqu. Ad litora torquent per conubia nostra inceptos himenaeos.",
                new DateTime(2024, 1, 14),
                new DateTime(2026, 12, 1),
                    ActivityType.Assignment,
                    moduleId["Programing Basics"].Id
                ),
                new Activity(
                    "Data Structures Assigment 1",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                new DateTime(2024, 1, 15),
                new DateTime(2026, 7, 12),
                    ActivityType.Assignment,
                    moduleId["Data Structures"].Id
                ),
            };
        context.Activities.AddRange(activities);
        await context.SaveChangesAsync();
    }
}

