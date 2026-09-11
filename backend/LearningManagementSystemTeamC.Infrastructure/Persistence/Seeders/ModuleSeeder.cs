using LearningManagementSystemTeamC.Domain.Modules;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Seeders;

public static class ModuleSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Modules.Any())
        {
            return;
        }

        var courseId = await context.Courses.ToDictionaryAsync(x => x.CourseName);

        var modules = new List<Module>
        {
            new Module(
                "Programing Basics",
                "Learn variables, data types and control flow.",
                new DateTime(2024, 1, 7),
                new DateTime(2024, 4, 1),
                courseId["Introduction to Programming"].Id),
            new Module(
                "Object-Oriented Programming",
                "Learn classes, objects and inheritance.",
                new DateTime(2024, 2, 1),
                new DateTime(2024, 3, 1),
                courseId["Introduction to Programming"].Id),

            new Module(
                "Programming Practices",
                "Learn clean code and common programming practices.",
                new DateTime(2024, 3, 1),
                new DateTime(2024, 4, 1),
                courseId["Introduction to Programming"].Id),

            new Module(
                "Data Structures",
                "Learn arrays, lists, stacks and queues.",
                new DateTime(2024, 4, 1),
                new DateTime(2024, 5, 1),
                courseId["Data Structures and Algorithms"].Id),

            new Module(
                "Algorithms",
                "Learn searching and sorting algorithms.",
                new DateTime(2024, 5, 1),
                new DateTime(2024, 6, 1),
                courseId["Data Structures and Algorithms"].Id),

            new Module(
                "Database Fundamentals",
                "Learn relational databases and database design.",
                new DateTime(2024, 8, 16),
                new DateTime(2024, 9, 1),
                courseId["Database Management Systems"].Id),

            new Module(
                "SQL",
                "Learn SQL queries and database operations.",
                new DateTime(2024, 9, 1),
                new DateTime(2024, 10, 1),
                courseId["Database Management Systems"].Id),

            new Module(
                "HTML and CSS",
                "Learn the fundamentals of building web pages.",
                new DateTime(2024, 10, 1),
                new DateTime(2024, 11, 1),
                courseId["Web Development"].Id),

            new Module(
                "JavaScript",
                "Learn JavaScript and browser-based programming.",
                new DateTime(2024, 11, 1),
                new DateTime(2024, 12, 1),
                courseId["Web Development"].Id),
            
            new Module(
                "Software Development Lifecycle",
                "Learn the different stages of software development.",
                new DateTime(2024, 12, 1),
                new DateTime(2025, 1, 15),
                courseId["Software Engineering"].Id),

            new Module(
                "Agile Development",
                "Learn Agile methodologies and Scrum.",
                new DateTime(2025, 1, 15),
                new DateTime(2025, 2, 1),
                courseId["Software Engineering"].Id),

            new Module(
                "Testing and Quality Assurance",
                "Learn software testing and quality practices.",
                new DateTime(2025, 2, 1),
                new DateTime(2025, 3, 1),
                courseId["Software Engineering"].Id)

        };

        context.Modules.AddRange(modules);

        await context.SaveChangesAsync();
    }
}