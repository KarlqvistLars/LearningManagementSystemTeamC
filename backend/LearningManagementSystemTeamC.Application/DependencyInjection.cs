using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
using LearningManagementSystemTeamC.Application.Courses.Commands.GetCourse;
using LearningManagementSystemTeamC.Application.Courses.Commands.GetCourses;
using Microsoft.Extensions.DependencyInjection;

namespace LearningManagementSystemTeamC.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Handlers
        services.AddScoped<CreateCourseHandler>();
        services.AddScoped<GetCoursesHandler>();
        services.AddScoped<GetCourseHandler>();

        // Validators
        services.AddScoped<CreateCourseValidator>();

        // or Featurebased
        //services.AddScoped<CreateCourseHandler>();
        //services.AddScoped<CreateCourseValidator>();

        return services;
    }
}