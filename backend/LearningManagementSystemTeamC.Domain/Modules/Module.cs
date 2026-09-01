using LearningManagementSystemTeamC.Domain.Courses;

namespace LearningManagementSystemTeamC.Domain.Modules;

public class Module
{
    public Guid Id { get; set; }
    public string ModuleName { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public Module(
        string moduleName,
        string description,
        DateTime startDate,
        DateTime endDate,
        Guid courseId)
    {
        Validate(
            moduleName,
            description,
            startDate,
            endDate,
            courseId);

        Id = Guid.NewGuid();
        ModuleName = moduleName;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        CourseId = courseId;
    }

    private static void Validate(
        string moduleName,
        string description,
        DateTime startDate,
        DateTime endDate,
        Guid courseId)
    {
        if (string.IsNullOrWhiteSpace(moduleName))
            throw new ArgumentException(
                "Course name cannot be empty.",
                nameof(moduleName));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException(
                "Description cannot be empty.",
                nameof(description));

        if (endDate <= startDate)
            throw new ArgumentException(
                "End date must be after start date.",
                nameof(endDate));
                
        if (courseId == Guid.Empty)
            throw new ArgumentException(
                "Course ID cannot be empty.",
                nameof(courseId));
    }
}