using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.ReadModels;
using LearningManagementSystemTeamC.Domain.Activities;
using LearningManagementSystemTeamC.Domain.Resources;
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
                x.Course.CourseName,

                _context.ActivityResources
                    .Where(ar => ar.ActivityId == x.Activity.Id)
                    .Join(
                        _context.Resources,
                        ar => ar.ResourceId,
                        resource => resource.Id,
                        (ar, resource) => resource)
                    .Where(resource => resource.Type == ResourceType.Submission)
                    .Select(resource => resource.CreatedBy)
                    .Distinct()
                    .Count(),

                _context.Enrollments
                    .Where(enrollment =>
                        enrollment.CourseId == x.Course.Id)
                    .Select(enrollment => enrollment.UserId)
                    .Distinct()
                    .Count()
            ))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AssignmentSubmissionReadModel>>
    GetAssignmentSubmissionsAsync(
        Guid activityId,
        CancellationToken cancellationToken)
    {
        var query =
            from activity in _context.Activities
            join module in _context.Modules
                on activity.ModuleId equals module.Id
            join enrollment in _context.Enrollments
                on module.CourseId equals enrollment.CourseId
            join userInfo in _context.UserInfos
                on enrollment.UserId equals userInfo.UserId
            where activity.Id == activityId
                  && activity.Type == ActivityType.Assignment
                  && enrollment.IsActive
            orderby userInfo.LastName, userInfo.FirstName
            select new
            {
                StudentId = enrollment.UserId,
                StudentFirstName = userInfo.FirstName,
                StudentLastName = userInfo.LastName,
                ActivityId = activity.Id,
                ActivityName = activity.ActivityName,
                EndDate = activity.EndDate,

                SubmissionId = _context.ActivityResources
                    .Where(ar => ar.ActivityId == activity.Id)
                    .Join(
                        _context.Resources,
                        ar => ar.ResourceId,
                        resource => resource.Id,
                        (ar, resource) => resource)
                    .Where(resource =>
                        resource.Type == ResourceType.Submission &&
                        resource.CreatedBy == enrollment.UserId)
                    .Select(resource => (Guid?)resource.Id)
                    .FirstOrDefault(),

                SubmittedAt = _context.ActivityResources
                    .Where(ar => ar.ActivityId == activity.Id)
                    .Join(
                        _context.Resources,
                        ar => ar.ResourceId,
                        resource => resource.Id,
                        (ar, resource) => resource)
                    .Where(resource =>
                        resource.Type == ResourceType.Submission &&
                        resource.CreatedBy == enrollment.UserId)
                    .Select(resource => (DateTime?)resource.CreatedAt)
                    .FirstOrDefault()
            };

        var results = await query.ToListAsync(cancellationToken);

        return results
            .Select(x => new AssignmentSubmissionReadModel(
                x.StudentId,
                x.StudentFirstName,
                x.StudentLastName,
                x.ActivityId,
                x.ActivityName,
                x.EndDate,
                x.SubmissionId,
                x.SubmittedAt))
            .ToList();
    }
}