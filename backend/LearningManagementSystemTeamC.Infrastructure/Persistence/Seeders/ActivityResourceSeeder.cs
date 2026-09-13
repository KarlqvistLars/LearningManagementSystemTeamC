using LearningManagementSystemTeamC.Domain.ActivityResources;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Seeders;

public static class ActivityResourceSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.ActivityResources.Any())
        {
            return;
        }

        var activityId = await context.Activities.ToDictionaryAsync(a => a.ActivityName, a => a);
        var resourceId = await context.Resources.ToDictionaryAsync(r => r.ResourceName, r => r);

        var activityResources = new List<ActivityResource>
        {
            new ActivityResource
            (
                activityId["Lecture A part 1"].Id,
                resourceId["Clean Code"].Id
            ),
            new ActivityResource
            (
                activityId["Lecture A part 1"].Id,
                resourceId["Introduction to Algorithms"].Id
            ),
                        new ActivityResource
            (
                activityId["Lecture A part 2"].Id,
                resourceId["Clean Code"].Id
            ),
            new ActivityResource
            (
                activityId["Lecture A part 2"].Id,
                resourceId["Introduction to Algorithms"].Id
            ),
                        new ActivityResource
            (
                activityId["Lecture A part 3"].Id,
                resourceId["Clean Code"].Id
            ),
            new ActivityResource
            (
                activityId["Lecture A part 3"].Id,
                resourceId["Introduction to Algorithms"].Id
            ),
                        new ActivityResource
            (
                activityId["Lecture A part 4"].Id,
                resourceId["Clean Code"].Id
            ),
            new ActivityResource
            (
                activityId["Lecture A part 4"].Id,
                resourceId["Introduction to Algorithms"].Id
            ),
                        new ActivityResource
            (
                activityId["Lecture A part 5"].Id,
                resourceId["Clean Code"].Id
            ),
            new ActivityResource
            (
                activityId["Lecture A part 5"].Id,
                resourceId["Introduction to Algorithms"].Id
            )
        };
        context.ActivityResources.AddRange(activityResources);
        await context.SaveChangesAsync();
    }
}
