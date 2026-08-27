namespace LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
    public record CreateCourseCommand(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate);



    //public string Name { get; init; } = string.Empty;
    //public string Description { get; init; } = string.Empty;
    //public DateTime StartDate { get; init; }
    //public DateTime EndDate { get; init; }