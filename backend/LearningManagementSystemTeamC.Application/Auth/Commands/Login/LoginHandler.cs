using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Common.Services;
using LearningManagementSystemTeamC.Application.Roles;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Roles;
using LearningManagementSystemTeamC.Domain.Users;
using Microsoft.Extensions.Options;

namespace LearningManagementSystemTeamC.Application.Auth.Commands.Login;

public class LoginHandler : ILoginHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly JwtSettings _jwtSettings;

    public LoginHandler(IUserRepository userRepository, IRoleRepository roleRepository, IJwtTokenService jwtTokenService, IOptions<JwtSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _jwtTokenService = jwtTokenService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<LoginResultDto> HandleAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(StringNormalizer.NormalizeEmail(command.Email), cancellationToken) ?? throw new NotFoundException(UserRules.UserNotFoundCode, UserRules.UserNotFoundMessage);
        if (!existingUser.IsActive)
            throw new UnauthorizedException(
                UserRules.AccountNotAvailableCode,
                UserRules.AccountNotAvailableMessage);

        var passwordDoesMatch = BCrypt.Net.BCrypt.Verify(
                command.Password,
                existingUser.PasswordHash
            );

        if (!passwordDoesMatch)
            throw new UnauthorizedException(
                UserRules.CredentialInvalidCode,
                UserRules.CredentialInvalidMessage
            );

        var existingRole = await _roleRepository.GetByIdAsync(existingUser.RoleId, cancellationToken) ?? throw new NotFoundException(RoleRules.RoleNotFoundCode, RoleRules.RoleNotFoundMessage);

        var jwtToken = _jwtTokenService.CreateToken(existingUser, existingRole);

        var expiresInMinutes = _jwtSettings.ExpiresInMinutes;

        var userDto = UserMapper.ToDto(existingUser, existingRole);

        return LoginResultMapper.ToDto(jwtToken, expiresInMinutes, userDto);
    }
}
