using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Resources.Queries.GetResourceById;

public interface IGetResourceByIdHandler
{
    Task<ResourceWithCreatorDto> HandleAsync(
        GetResourceByIdQuery query,
        CancellationToken cancellationToken);
}
