using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.Courses;
using System.Linq.Expressions;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class CourseMapper
{
    public static CourseDto CourseToDto(Course course) =>
        new CourseDto(
            course.Id,
            course.CourseName,
            course.Description,
            course.StartDate,
            course.EndDate);
}