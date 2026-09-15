using LearningManagementSystemTeamC.Application.ActivityResources;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.UserInfos;

namespace LearningManagementSystemTeamC.Application.Resources.Queries.GetResourceById
{
    public class GetResourceByIdHandler : IGetResourceByIdHandler
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IUserInfoRepository _userInfoRepository;

        public GetResourceByIdHandler(
            IResourceRepository resourceRepository,
            IUserInfoRepository userInfoRepository)
        {
            _resourceRepository = resourceRepository;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<ResourceWithCreatorDto> HandleAsync(
            GetResourceByIdQuery query,
            CancellationToken cancellationToken)
        {
            var resource =
                await _resourceRepository.GetResourceByIdAsync(
                query.ResourceId,
                cancellationToken);

            return ResourceMapper.ToResourceWithCreatorDto(
                resource,
                await _userInfoRepository.GetByUserIdAsync(
                    resource.CreatedBy,
                    cancellationToken)
                ?? throw new NotFoundException(
                    UserInfoRules.UserInfoNotFoundCode,
                    UserInfoRules.UserInfoNotFoundMessage));
        }
    }
}
