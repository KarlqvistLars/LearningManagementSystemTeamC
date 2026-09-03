using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Domain.Courses;

public class Course
{
    public Guid Id { get; private set; }

    public string CourseName { get; private set; }

    public string Description { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Course(
        string courseName,
        string description,
        DateTime startDate,
        DateTime endDate)
    {
        Validate(
            courseName,
            description,
            startDate,
            endDate);

        Id = Guid.NewGuid();
        CourseName = courseName;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        CreatedAt = DateTime.UtcNow;
    }

    private static void Validate(
        string courseName,
        string description,
        DateTime startDate,
        DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(courseName))
            throw new ArgumentException(
                "Course name cannot be empty.",
                nameof(courseName));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException(
                "Description cannot be empty.",
                nameof(description));

        if (endDate <= startDate)
            throw new ArgumentException(
                "End date must be after start date.",
                nameof(endDate));
    }
}
