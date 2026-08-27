using Microsoft.Extensions.DependencyInjection;
using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;

namespace LearningManagementSystemTeamC.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Handlers
        services.AddScoped<CreateCourseHandler>();

        // Validators
        services.AddScoped<CreateCourseValidator>();

        // or Featurebased
        //services.AddScoped<CreateCourseHandler>();
        //services.AddScoped<CreateCourseValidator>();

        return services;
    }
}