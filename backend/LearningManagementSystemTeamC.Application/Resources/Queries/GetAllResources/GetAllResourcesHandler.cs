using LearningManagementSystemTeamC.Application.ActivityResources;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Resources;

namespace LearningManagementSystemTeamC.Application.Resources.Queries.GetAllResources;

public class GetAllResourcesHandler : IGetAllResourcesHandler
{
    private readonly IResourceRepository _resourceRepository;

    private readonly IUnitOfWork _unitOfWork;

    public GetAllResourcesHandler(
        IResourceRepository resourceRepository,
        IUnitOfWork unitOfWork)
    {
        _resourceRepository = resourceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ResourceDto>> Handle(
        GetAllResourcesQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _resourceRepository
    .GetAllResourcesAsync(
        query.ResourceId,
        cancellationToken)
            ?? throw new DomainException(ResourceRules.ResourceCode, ResourceRules.ResourceNotFound);

        return result
            .Select(ResourceMapper.ResourceToDto)
            .ToList();
    }
}
