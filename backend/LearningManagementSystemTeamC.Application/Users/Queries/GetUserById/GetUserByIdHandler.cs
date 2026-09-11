using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Roles;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Roles;
using LearningManagementSystemTeamC.Domain.UserInfos;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Users.Queries.GetUserById;

public class GetUserByIdHandler : IGetUserByIdHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserInfoRepository _userInfoRepository;

    public GetUserByIdHandler(IUserRepository userRepository, IRoleRepository roleRepository, IUserInfoRepository userInfoRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userInfoRepository = userInfoRepository;
    }

    public async Task<UserDto> HandleAsync(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByIdAsync(query.Id, cancellationToken) ?? throw new NotFoundException(
                UserRules.UserNotFoundCode,
                UserRules.UserNotFoundMessage);

        var existingRole = await _roleRepository.GetActiveByIdAsync(
            existingUser.RoleId,
            cancellationToken)
            ?? throw new NotFoundException(
                RoleRules.RoleNotFoundCode,
                RoleRules.RoleNotFoundMessage);

        var existingUserInfo = await _userInfoRepository.GetByUserIdAsync(
            existingUser.Id,
            cancellationToken)
            ?? throw new NotFoundException(
                UserInfoRules.UserInfoNotFoundCode,
                UserInfoRules.UserInfoNotFoundMessage);

        return UserMapper.ToDto(existingUser, existingRole, existingUserInfo);
    }
}
