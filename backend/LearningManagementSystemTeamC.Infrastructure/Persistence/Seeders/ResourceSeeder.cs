using LearningManagementSystemTeamC.Domain.Resources;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Seeders;

public static class ResourceSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Resources.Any())
        {
            return;
        }

        var teacher = context.Users
            .FirstOrDefault(u => u.Email == "admin@lms.com");

        if (teacher == null)
        {
            return;
        }

        var resources = new List<Resource>
        {
            new Resource(
                "Clean Code",
                "Even bad code can function. But if code isn’t clean, it can bring a development organization to its knees. Every year, countless hours and significant resources are lost because of poorly written code. But it doesn’t have to be that way. Noted software expert Robert C. Martin presents a revolutionary paradigm with Clean Code.",
                "https://www.adlibris.com/sv/bok/clean-code-9780132350884",
                ResourceType.CourseLiterature,
                teacher.Id
            ),

            new Resource(
                "Introduction to Algorithms",
                "Introduction to Algorithms is a comprehensive textbook covering a broad range of algorithms in depth, yet makes their design and analysis accessible to all levels of readers.",
                "https://www.cs.mcgill.ca/~akroit/math/compsci/Cormen%20Introduction%20to%20Algorithms.pdf",
                ResourceType.ELearningLiterature,
                teacher.Id
            )
        };

        context.Resources.AddRange(resources);

        await context.SaveChangesAsync();
    }
}