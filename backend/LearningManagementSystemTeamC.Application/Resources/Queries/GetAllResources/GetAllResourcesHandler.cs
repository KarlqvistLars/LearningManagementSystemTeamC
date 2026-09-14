using LearningManagementSystemTeamC.Application.ActivityResources;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;

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
        cancellationToken);

        return result
            .Select(ResourceMapper.ResourceToDto)
            .ToList();
    }
}
