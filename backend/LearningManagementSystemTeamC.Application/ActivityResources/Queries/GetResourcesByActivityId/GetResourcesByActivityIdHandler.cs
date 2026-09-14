using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Domain.ActivityResources;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Application.ActivityResources.Queries.GetResourcesByActivityId;

public class GetResourcesByActivityIdHandler : IGetResourcesByActivityIdHandler
{
    private readonly IActivityResourceRepository _activityResourceRepository;

    private readonly IUnitOfWork _unitOfWork;

    public GetResourcesByActivityIdHandler(
        IActivityResourceRepository activityResourceRepository,
        IUnitOfWork unitOfWork)
    {
        _activityResourceRepository = activityResourceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ActivityResourceDto>> Handle(
        GetResourcesByActivityIdQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _activityResourceRepository
    .GetActivityResourcesByActivityIdAsync(
        query.ActivityId,
        cancellationToken)
            ?? throw new DomainException(ActivityResourceRules.ActivityResourceCode, ActivityResourceRules.ActivityResourcesNotFound);

        return result
            .Select(ActivityResourceMapper.ActivityResourceToDto)
            .ToList();
    }
}
