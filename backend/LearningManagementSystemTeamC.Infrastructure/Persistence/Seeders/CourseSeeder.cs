using LearningManagementSystemTeamC.Domain.Courses;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Seeders;

public static class CourseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Courses.Any())
        {
            return;
        }

        var courses = new List<Course>
        {
            new Course(
                "Introduction to Programming",
                "Learn the basics of programming.",
                new DateTime(2024, 1, 7),
                new DateTime(2024, 4, 1)),

            new Course(
                "Data Structures and Algorithms",
                "Explore data structures and algorithms.",
                new DateTime(2024, 4, 1),
                new DateTime(2024, 6, 1)),

            new Course(
                "Database Management Systems",
                "Understand database concepts and SQL.",
                new DateTime(2024, 8, 16),
                new DateTime(2024, 10, 1)),

            new Course(
                "Web Development",
                "Build web applications using modern technologies.",
                new DateTime(2024, 10, 1),
                new DateTime(2024, 12, 1)),

            new Course(
                "Software Engineering",
                "Learn software development methodologies.",
                new DateTime(2024, 12, 1),
                new DateTime(2025, 3, 1))
        };

        context.Courses.AddRange(courses);

        await context.SaveChangesAsync();
    }
}