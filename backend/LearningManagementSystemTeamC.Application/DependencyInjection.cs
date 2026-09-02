using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourses;
using Microsoft.Extensions.DependencyInjection;

namespace LearningManagementSystemTeamC.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Handlers
        services.AddScoped<ICreateCourseHandler, CreateCourseHandler>();
        services.AddScoped<IGetCoursesHandler, GetCoursesHandler>();
        services.AddScoped<IGetCourseByIdHandler, GetCourseByIdHandler>();

        // Validators
        services.AddScoped<IValidator<CreateCourseCommand>, CreateCourseValidator>();

        return services;
    }
}