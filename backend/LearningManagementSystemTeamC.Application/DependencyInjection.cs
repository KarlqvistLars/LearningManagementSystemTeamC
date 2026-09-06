using LearningManagementSystemTeamC.Application.Auth.Commands.Login;
using LearningManagementSystemTeamC.Application.Auth.Commands.RegisterUser;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourses;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModule;
using LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;
using LearningManagementSystemTeamC.Application.Users.Queries.GetUserById;
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
        services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
        services.AddScoped<IGetModuleHandler, GetModuleHandler>();
        services.AddScoped<ILoginHandler, LoginHandler>();

        // Validators
        services.AddScoped<IValidator<CreateCourseCommand>, CreateCourseValidator>();
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserValidator>();
        services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserValidator>();
        services.AddScoped<IValidator<LoginCommand>, LoginValidator>();

        return services;
    }
}