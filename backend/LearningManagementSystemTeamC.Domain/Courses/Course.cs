namespace LearningManagementSystemTeamC.Domain.Courses;

public class Course
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Course(
        string name,
        string description,
        DateTime startDate,
        DateTime endDate)
    {
        // validations

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        CreatedAt = DateTime.UtcNow;
    }
}
