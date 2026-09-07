using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Modules;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModule;

public class GetActivitiesByModuleHandler : IGetActivitiesByModuleHandler
{
    private readonly IActivityRepository _activityRepository;
    private readonly IModuleRepository _moduleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetActivitiesByModuleHandler(
        IActivityRepository activityRepository,
        IModuleRepository moduleRepository,
        IUnitOfWork unitOfWork)
    {
        _activityRepository = activityRepository;
        _moduleRepository = moduleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ActivityDto>> Handle(
        GetActivitiesByModuleQuery query,
        CancellationToken cancellationToken)
    {
        var module = await _moduleRepository.GetByIdAsync(query.ModuleId, cancellationToken)
            ?? throw new DomainException(ModuleRules.ModuleNotFoundCode, ModuleRules.ModuleNotFoundMessage);

        var activities = await _activityRepository.GetActivitiesByModuleIdAsync(query.ModuleId);

        return activities
            .Select(ActivityMapper.ActivityToDto)
            .ToList();
    }
}