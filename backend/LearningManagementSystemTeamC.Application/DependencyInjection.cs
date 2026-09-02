using Microsoft.Extensions.DependencyInjection;
using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
using LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;

namespace LearningManagementSystemTeamC.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Handlers
        services.AddScoped<CreateCourseHandler>();
        services.AddScoped<CreateUserHandler>();

        //// Validators
        services.AddScoped<CreateCourseValidator>();
        services.AddScoped<CreateUserValidator>();

        // or Featurebased
        //services.AddScoped<CreateCourseHandler>();
        //services.AddScoped<CreateCourseValidator>();

        return services;
    }
}