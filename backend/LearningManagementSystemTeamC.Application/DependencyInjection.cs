using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModuleId;
using LearningManagementSystemTeamC.Application.Auth.Commands.ForgotPassword;
using LearningManagementSystemTeamC.Application.Auth.Commands.Login;
using LearningManagementSystemTeamC.Application.Auth.Commands.RegisterUser;
using LearningManagementSystemTeamC.Application.Auth.Commands.ResetPassword;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourses;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCoursesByIdRange;
using LearningManagementSystemTeamC.Application.Enrollments.Commands.EnrollUserInCourse;
using LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByCourseId;
using LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByUserId;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModule;
using LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;
using LearningManagementSystemTeamC.Application.Users.Commands.UpdateUser;
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
        services.AddScoped<IGetEnrollmentsByCourseIdHandler, GetEnrollmentsByCourseIdHandler>();
        services.AddScoped<IGetActivitiesByModuleIdHandler, GetActivitiesByModuleIdHandler>();
        services.AddScoped<IGetEnrollmentsByCourseIdHandler, GetEnrollmentsByCourseIdHandler>();
        services.AddScoped<ICreateUserHandler, CreateUserHandler>();
        services.AddScoped<IGetUserByIdHandler, GetUserByIdHandler>();
        services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
        services.AddScoped<IGetModuleHandler, GetModuleHandler>();
        services.AddScoped<ILoginHandler, LoginHandler>();
        services.AddScoped<IEnrollUserInCourseHandler, EnrollUserInCourseHandler>();
        services.AddScoped<IGetEnrollmentsByUserIdHandler, GetEnrollmentsByUserIdHandler>();
        services.AddScoped<IGetCoursesByIdRangeHandler, GetCoursesByIdRangeHandler>();
        services.AddScoped<IUpdateUserHandler, UpdateUserHandler>();
        services.AddScoped<IForgotPasswordHandler, ForgotPasswordHandler>();
        services.AddScoped<IResetPasswordHandler, ResetPasswordHandler>();

        // Validators
        services.AddScoped<IValidator<CreateCourseCommand>, CreateCourseValidator>();
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserValidator>();
        services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserValidator>();
        services.AddScoped<IValidator<LoginCommand>, LoginValidator>();
        services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserValidator>();
        services.AddScoped<IValidator<ForgotPasswordCommand>, ForgotPasswordValidator>();
        services.AddScoped<IValidator<ResetPasswordCommand>, ResetPasswordValidator>();

        return services;
    }
}
