using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.ReadModels;
using LearningManagementSystemTeamC.Domain.Activities;
using LearningManagementSystemTeamC.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class ActivityReadRepository : IActivityReadRepository
{
    private readonly ApplicationDbContext _context;

    public ActivityReadRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ActivityDetailsReadModel>> GetAssignmentsAsync(
        Guid userId,
        string roleCode,
        CancellationToken cancellationToken)
    {
        var query = _context.Activities
            .Where(activity => activity.Type == ActivityType.Assignment)
            .Join(
                _context.Modules,
                activity => activity.ModuleId,
                module => module.Id,
                (activity, module) => new
                {
                    Activity = activity,
                    Module = module
                });

        if (roleCode == RoleRules.StudentRoleCode)
        {
            query = query.Where(x =>
                _context.Enrollments.Any(enrollment =>
                    enrollment.UserId == userId &&
                    enrollment.CourseId == x.Module.CourseId));
        }

        return await query
            .Join(
                _context.Courses,
                x => x.Module.CourseId,
                course => course.Id,
                (x, course) => new
                {
                    x.Activity,
                    x.Module,
                    Course = course
                })
            .OrderBy(x => x.Activity.EndDate)
            .Select(x => new ActivityDetailsReadModel(
                x.Activity.Id,
                x.Activity.ActivityName,
                (int)x.Activity.Type,
                x.Activity.Description,
                x.Activity.StartDate,
                x.Activity.EndDate,
                x.Module.Id,
                x.Module.ModuleName,
                x.Course.Id,
                x.Course.CourseName))
            .ToListAsync(cancellationToken);
    }
}