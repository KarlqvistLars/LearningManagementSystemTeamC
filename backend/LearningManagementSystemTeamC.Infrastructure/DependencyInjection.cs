using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses;
using LearningManagementSystemTeamC.Infrastructure.Persistence;
using LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;
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

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}