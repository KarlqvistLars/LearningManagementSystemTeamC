using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.Courses;
using System.Linq.Expressions;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class CourseMapper
{
    public static readonly Expression<Func<Course, CourseDto>> CourseToDto =
    course => new CourseDto(
        course.Id,
        course.CourseName,
        course.Description,
        course.StartDate,
        course.EndDate);
}