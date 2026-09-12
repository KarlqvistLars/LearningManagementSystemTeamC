using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModuleId;
using LearningManagementSystemTeamC.Application.Auth.Commands.ForgotPassword;
using LearningManagementSystemTeamC.Application.Auth.Commands.Login;
using LearningManagementSystemTeamC.Application.Auth.Commands.RegisterUser;
using LearningManagementSystemTeamC.Application.Auth.Commands.ResetPassword;
using LearningManagementSystemTeamC.Application.ChatRooms.Commands.AddChatRoomMember;
using LearningManagementSystemTeamC.Application.ChatRooms.Commands.CreateChatRoom;
using LearningManagementSystemTeamC.Application.ChatRooms.Commands.DeleteChatRoom;
using LearningManagementSystemTeamC.Application.ChatRooms.Queries.GetChatRoomById;
using LearningManagementSystemTeamC.Application.ChatRooms.Queries.GetMyChatRooms;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
using LearningManagementSystemTeamC.Application.Courses.Commands.UpdateCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourses;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCoursesByIdRange;
using LearningManagementSystemTeamC.Application.Enrollments.Commands.EnrollUserInCourse;
using LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByCourseId;
using LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByUserId;
using LearningManagementSystemTeamC.Application.Modules.Commands.CreateModule;
using LearningManagementSystemTeamC.Application.Modules.Commands.EditModule;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModuleById;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModules;
using LearningManagementSystemTeamC.Application.Roles.Queries.GetRoles;
using LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;
using LearningManagementSystemTeamC.Application.Users.Commands.DeleteUser;
using LearningManagementSystemTeamC.Application.Users.Commands.ToggleUserStatus;
using LearningManagementSystemTeamC.Application.Users.Commands.UpdateUser;
using LearningManagementSystemTeamC.Application.Users.Queries.GetUserById;
using LearningManagementSystemTeamC.Application.Users.Queries.GetUsers;
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
        services.AddScoped<ICreateUserHandler, CreateUserHandler>();
        services.AddScoped<IGetUserByIdHandler, GetUserByIdHandler>();
        services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
        services.AddScoped<IGetModulesHandler, GetModulesHandler>();
        services.AddScoped<IGetModuleByIdHandler, GetModuleByIdHandler>();
        services.AddScoped<ICreateModuleHandler, CreateModuleHandler>();
        services.AddScoped<IEditModuleHandler, EditModuleHandler>();
        services.AddScoped<ILoginHandler, LoginHandler>();
        services.AddScoped<IEnrollUserInCourseHandler, EnrollUserInCourseHandler>();
        services.AddScoped<IGetEnrollmentsByUserIdHandler, GetEnrollmentsByUserIdHandler>();
        services.AddScoped<IGetCoursesByIdRangeHandler, GetCoursesByIdRangeHandler>();
        services.AddScoped<IUpdateUserHandler, UpdateUserHandler>();
        services.AddScoped<IForgotPasswordHandler, ForgotPasswordHandler>();
        services.AddScoped<IResetPasswordHandler, ResetPasswordHandler>();
        services.AddScoped<IGetUsersHandler, GetUsersHandler>();
        services.AddScoped<IDeleteUserHandler, DeleteUserHandler>();
        services.AddScoped<IToggleUserStatusHandler, ToggleUserStatusHandler>();
        services.AddScoped<IGetRolesHandler, GetRolesHandler>();
        services.AddScoped<IUpdateCourseHandler, UpdateCourseHandler>();
        services.AddScoped<ICreateChatRoomHandler, CreateChatRoomHandler>();
        services.AddScoped<IGetChatRoomByIdHandler, GetChatRoomByIdHandler>();
        services.AddScoped<IGetMyChatRoomsHandler, GetMyChatRoomsHandler>();
        services.AddScoped<IDeleteChatRoomHandler, DeleteChatRoomHandler>();
        services.AddScoped<IAddChatRoomMemberHandler, AddChatRoomMemberHandler>();

        // Validators
        services.AddScoped<IValidator<CreateCourseCommand>, CreateCourseValidator>();
        services.AddScoped<IValidator<CreateModuleCommand>, CreateModuleValidator>();
        services.AddScoped<IValidator<EditModuleCommand>, EditModuleValidator>();
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserValidator>();
        services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserValidator>();
        services.AddScoped<IValidator<LoginCommand>, LoginValidator>();
        services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserValidator>();
        services.AddScoped<IValidator<ForgotPasswordCommand>, ForgotPasswordValidator>();
        services.AddScoped<IValidator<ResetPasswordCommand>, ResetPasswordValidator>();
        services.AddScoped<IValidator<UpdateCourseCommand>, UpdateCourseValidator>();
        services.AddScoped<IValidator<CreateChatRoomCommand>, CreateChatRoomValidator>();

        return services;
    }
}
