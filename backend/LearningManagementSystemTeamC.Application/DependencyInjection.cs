using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourses;
using LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;
using LearningManagementSystemTeamC.Application.Users.Queries.GetUserById;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModule;
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
        services.AddScoped<ICreateUserHandler, CreateUserHandler>();
        services.AddScoped<IGetUserByIdHandler, GetUserByIdHandler>();
        services.AddScoped<IGetModuleHandler, GetModuleHandler>();

        // Validators
        services.AddScoped<IValidator<CreateCourseCommand>, CreateCourseValidator>();
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserValidator>();

        return services;
    }
}