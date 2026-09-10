using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Roles;

namespace LearningManagementSystemTeamC.Application.Users.Queries.GetUsers
{
    public class GetUsersHandler : IGetUsersHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserInfoRepository _userInfoRepository;

        public GetUsersHandler(IUserRepository userRepository, IRoleRepository roleRepository, IUserInfoRepository userInfoRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<IReadOnlyList<UserDto>> HandleAsync(
            GetUsersQuery query,
            CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);
            var roles = await _roleRepository.GetAllAsync(cancellationToken);
            var userInfos = await _userInfoRepository.GetAllAsync(cancellationToken);

            var roleLookup = roles.ToDictionary(role => role.Id);
            var userInfoLookup = userInfos.ToDictionary(userInfo => userInfo.UserId);

            return users
                .Select(user =>
                {
                    var role = roleLookup[user.RoleId];
                    var userInfo = userInfoLookup[user.Id];

                    return UserMapper.ToDto(
                        user,
                        role,
                        userInfo.FirstName,
                        userInfo.LastName);
                })
                .ToList();
        }
    }
}
