using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses;
using LearningManagementSystemTeamC.Application.Modules;
using LearningManagementSystemTeamC.Infrastructure.Persistence;
using LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LearningManagementSystemTeamC.Application.Activities;

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

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IActivityRepository, ActivityRepository>();  

        return services;
    }
}