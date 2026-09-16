using LearningManagementSystemTeamC.Application.ActivityResources;
using LearningManagementSystemTeamC.Application.ActivityResources.Queries.GetResourcesByActivityId;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.UserInfos;

namespace LearningManagementSystemTeamC.Application.Resources.Queries.GetResourcesByActivityId;

public class GetResourcesByActivityIdHandler : IGetResourcesByActivityIdHandler
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IUserInfoRepository _userInfoRepository;

    public GetResourcesByActivityIdHandler(
        IResourceRepository resourceRepository,
        IUserInfoRepository userInfoRepository)
    {
        _resourceRepository = resourceRepository;
        _userInfoRepository = userInfoRepository;
    }

    public async Task<IReadOnlyList<ResourceWithCreatorDto>> HandleAsync(
    GetResourcesByActivityIdQuery query,
    CancellationToken cancellationToken)
    {
        var resources =
            await _resourceRepository.GetResourcesByActivityIdAsync(
                query.ActivityId,
                cancellationToken);

        var result = new List<ResourceWithCreatorDto>();

        foreach (var resource in resources)
        {
            var userInfo =
                await _userInfoRepository.GetByUserIdAsync(
                    resource.CreatedBy,
                    cancellationToken)
                ?? throw new NotFoundException(
                    UserInfoRules.UserInfoNotFoundCode,
                    UserInfoRules.UserInfoNotFoundMessage);

            result.Add(
                ResourceMapper.ToResourceWithCreatorDto(
                    resource,
                    userInfo));
        }

        return result;
    }
}