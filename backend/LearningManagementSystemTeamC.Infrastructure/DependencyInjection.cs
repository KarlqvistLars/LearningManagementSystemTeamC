using LearningManagementSystemTeamC.Application.Activities;
using LearningManagementSystemTeamC.Application.Auth;
using LearningManagementSystemTeamC.Application.ChatRooms;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses;
using LearningManagementSystemTeamC.Application.Enrollments;
using LearningManagementSystemTeamC.Application.Modules;
using LearningManagementSystemTeamC.Application.Roles;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Infrastructure.Email;
using LearningManagementSystemTeamC.Infrastructure.Persistence;
using LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;
using LearningManagementSystemTeamC.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningManagementSystemTeamC.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Default"))
        );

        services.Configure<BrevoSettings>(
            config.GetSection("Brevo"));

        services.AddHttpClient<IEmailService, BrevoEmailService>(client =>
        {
            client.BaseAddress = new Uri("https://api.brevo.com/");
            client.DefaultRequestHeaders.Add(
                "api-key",
                config["Brevo:ApiKey"]);
        });

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IActivityRepository, ActivityRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IUserInfoRepository, UserInfoRepository>();
        services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();
        services.AddScoped<IPasswordResetTokenService, PasswordResetTokenService>();
        services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
        services.AddScoped<IChatRoomReadRepository, ChatRoomReadRepository>();
        return services;
    }
}