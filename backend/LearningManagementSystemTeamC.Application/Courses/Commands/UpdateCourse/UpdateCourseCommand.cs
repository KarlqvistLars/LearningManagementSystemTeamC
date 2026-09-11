namespace LearningManagementSystemTeamC.Application.Courses.Commands.UpdateCourse;
    public record UpdateCourseCommand(
    Guid Id,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate);