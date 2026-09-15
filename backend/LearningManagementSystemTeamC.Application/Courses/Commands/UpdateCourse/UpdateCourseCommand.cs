namespace LearningManagementSystemTeamC.Application.Courses.Commands.UpdateCourse;
    public record UpdateCourseCommand(
    Guid Id,
    string CourseName,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    DateTime CreatedAt);