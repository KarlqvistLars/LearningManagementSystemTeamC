using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses;
using LearningManagementSystemTeamC.Infrastructure.Persistence;
using LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LearningManagementSystemTeamC.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}