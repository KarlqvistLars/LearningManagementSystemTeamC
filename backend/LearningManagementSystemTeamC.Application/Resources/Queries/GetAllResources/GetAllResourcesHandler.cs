using LearningManagementSystemTeamC.Application.ActivityResources;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Resources;

namespace LearningManagementSystemTeamC.Application.Resources.Queries.GetAllResources;

public class GetAllResourcesHandler : IGetAllResourcesHandler
{
    private readonly IResourceRepository _resourceRepository;
    public GetAllResourcesHandler(
        IResourceRepository resourceRepository
        )
    {
        _resourceRepository = resourceRepository;
    }

    public async Task<IReadOnlyList<ResourceDto>> Handle(
        GetAllResourcesQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _resourceRepository
    .GetAllResourcesAsync(
        cancellationToken)
            ?? throw new DomainException(ResourceRules.ResourceCode, ResourceRules.ResourceNotFound);

        return result
            .Select(ResourceMapper.ResourceToDto)
            .ToList();
    }
}
