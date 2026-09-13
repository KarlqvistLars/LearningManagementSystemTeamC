using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Enrollments;
using LearningManagementSystemTeamC.Application.Modules;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Enrollments;
using LearningManagementSystemTeamC.Domain.Modules;
using LearningManagementSystemTeamC.Domain.Roles;

namespace LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModuleId;

public class GetActivitiesByModuleIdHandler : IGetActivitiesByModuleIdHandler
{
    private readonly IActivityRepository _activityRepository;
    private readonly IModuleRepository _moduleRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetActivitiesByModuleIdHandler(
        IActivityRepository activityRepository,
        IModuleRepository moduleRepository,
        IEnrollmentRepository enrollmentRepository,
        IUnitOfWork unitOfWork)
    {
        _activityRepository = activityRepository;
        _moduleRepository = moduleRepository;
        _enrollmentRepository = enrollmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ActivityDto>> Handle(
        GetActivitiesByModuleIdQuery query,
        CancellationToken cancellationToken)
    {
        var module = await _moduleRepository.GetByIdAsync(query.ModuleId, cancellationToken)
            ?? throw new DomainException(ModuleRules.ModuleNotFoundCode, ModuleRules.ModuleNotFoundMessage);

        if (query.RoleCode == RoleRules.StudentRoleCode)
        {
            var enrollment = await _enrollmentRepository.GetByUserIdAndCourseIdAsync(
                query.UserId,
                module.CourseId,
                cancellationToken);

            if (enrollment == null)
            {
                throw new UnauthorizedException(EnrollmentRules.UserNotEnrolledCode, EnrollmentRules.UserNotEnrolledMessage);
            }
        }

        var activities = await _activityRepository.GetActivitiesByModuleIdAsync(query.ModuleId);

        return activities
            .Select(ActivityMapper.ActivityToDto)
            .ToList();
    }
}