namespace LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
    public record CreateCourseCommand(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate);