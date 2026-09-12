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

        var activityResources = new List<ActivityResource>
        {
            new ActivityResource
            (
                "Clean Code",
                "Even bad code can function. But if code isn’t clean, it can bring a development organization to its knees. Every year, countless hours and significant resources are lost because of poorly written code. But it doesn’t have to be that way. Noted software expert Robert C. Martin presents a revolutionary paradigm with Clean Code",
                "https://www.adlibris.com/sv/bok/clean-code-9780132350884",
                new DateTime(2024, 1, 1),
                activityId["Lecture A part 1"].Type,
                activityId["Lecture A part 1"].Id,
                activityId["Lecture A part 1"].ModuleId
            ),
            new ActivityResource
            (
                 "Introduction to Algorithms",
                "Introduction to Algorithms is a comprehensive textbook covering a broad range of algorithms in depth, yet makes their design and analysis accessible to all levels of readers.",
                "https://www.cs.mcgill.ca/~akroit/math/compsci/Cormen%20Introduction%20to%20Algorithms.pdf",
                new DateTime(2024, 1, 1),
                activityId["Lecture A part 1"].Type,
                activityId["Lecture A part 1"].Id,
                activityId["Lecture A part 1"].ModuleId
             ),
                        new ActivityResource
            (
                "Clean Code",
                "Even bad code can function. But if code isn’t clean, it can bring a development organization to its knees. Every year, countless hours and significant resources are lost because of poorly written code. But it doesn’t have to be that way. Noted software expert Robert C. Martin presents a revolutionary paradigm with Clean Code",
                "https://www.adlibris.com/sv/bok/clean-code-9780132350884",
                new DateTime(2024, 1, 1),
                activityId["Lecture A part 2"].Type,
                activityId["Lecture A part 2"].Id,
                activityId["Lecture A part 2"].ModuleId
            ),
            new ActivityResource
            (
                 "Introduction to Algorithms",
                "Introduction to Algorithms is a comprehensive textbook covering a broad range of algorithms in depth, yet makes their design and analysis accessible to all levels of readers.",
                "https://www.cs.mcgill.ca/~akroit/math/compsci/Cormen%20Introduction%20to%20Algorithms.pdf",
                new DateTime(2024, 1, 1),
                activityId["Lecture A part 2"].Type,
                activityId["Lecture A part 2"].Id,
                activityId["Lecture A part 2"].ModuleId
             ),
                        new ActivityResource
            (
                "Clean Code",
                "Even bad code can function. But if code isn’t clean, it can bring a development organization to its knees. Every year, countless hours and significant resources are lost because of poorly written code. But it doesn’t have to be that way. Noted software expert Robert C. Martin presents a revolutionary paradigm with Clean Code",
                "https://www.adlibris.com/sv/bok/clean-code-9780132350884",
                new DateTime(2024, 1, 1),
                activityId["Lecture A part 3"].Type,
                activityId["Lecture A part 3"].Id,
                activityId["Lecture A part 3"].ModuleId
            ),
            new ActivityResource
            (
                 "Introduction to Algorithms",
                "Introduction to Algorithms is a comprehensive textbook covering a broad range of algorithms in depth, yet makes their design and analysis accessible to all levels of readers.",
                "https://www.cs.mcgill.ca/~akroit/math/compsci/Cormen%20Introduction%20to%20Algorithms.pdf",
                new DateTime(2024, 1, 1),
                activityId["Lecture A part 3"].Type,
                activityId["Lecture A part 3"].Id,
                activityId["Lecture A part 3"].ModuleId
             )
        };
        context.ActivityResources.AddRange(activityResources);
        await context.SaveChangesAsync();
    }
}
