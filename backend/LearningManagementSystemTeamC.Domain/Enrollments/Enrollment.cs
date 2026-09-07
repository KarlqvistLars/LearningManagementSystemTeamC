namespace LearningManagementSystemTeamC.Domain.Enrollments;

public class Enrollment
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; } = Guid.Empty;

    public Guid CourseId { get; private set; } = Guid.Empty;

    public DateTime EnrolledAt { get; private set; } = DateTime.UtcNow;

    public bool IsActive { get; private set; }

    public void Disable()
    {
        IsActive = false;
    }

    private Enrollment() { }

    public Enrollment(Guid userId, Guid courseId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        CourseId = courseId;
        EnrolledAt = DateTime.UtcNow;
        IsActive = true;
    }
}

