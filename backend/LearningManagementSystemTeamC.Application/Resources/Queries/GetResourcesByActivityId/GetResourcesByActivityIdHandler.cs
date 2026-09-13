using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Resources;

namespace LearningManagementSystemTeamC.Application.ActivityResources.Queries.GetResourcesByActivityId;

public class GetResourcesByActivityIdHandler : IGetResourcesByActivityIdHandler
{
    private readonly IResourceRepository _resourceRepository;

    public GetResourcesByActivityIdHandler(
        IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }

    public async Task<IReadOnlyList<ResourceDto>> Handle(
        GetResourcesByActivityIdQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _resourceRepository
    .GetResourcesByActivityIdAsync(
        query.ActivityId,
        cancellationToken)
            ?? throw new DomainException(ResourceRules.ResourceCode, ResourceRules.ResourceNotFound);

        return result
            .Select(ResourceMapper.ResourceToDto)
            .ToList();
    }
}
