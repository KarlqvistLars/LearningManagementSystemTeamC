using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Roles;

namespace LearningManagementSystemTeamC.Application.Users.Queries.GetActiveUsersByRole
{
    public class GetActiveUsersByRoleHandler : IGetActiveUsersByRoleHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserInfoRepository _userInfoRepository;

        public GetActiveUsersByRoleHandler(IUserRepository userRepository, IRoleRepository roleRepository, IUserInfoRepository userInfoRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<IReadOnlyList<UserSimplifiedDto>> HandleAsync(
            GetActiveUsersByRoleQuery query,
            CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);
            var roles = await _roleRepository.GetAllAsync(cancellationToken);
            var userInfos = await _userInfoRepository.GetAllAsync(cancellationToken);

            var roleLookup = roles.ToDictionary(role => role.Id);
            var userInfoLookup = userInfos.ToDictionary(userInfo => userInfo.UserId);

            return users
                .Where(user => user.IsActive && roleLookup[user.RoleId].Name.Equals(query.Role, StringComparison.OrdinalIgnoreCase))
                .Select(user =>
                {
                    var userInfo = userInfoLookup[user.Id];

                    return UserMapper.ToLightDto(
                        user,
                        userInfo);
                })
                .ToList();
        }
    }
}
