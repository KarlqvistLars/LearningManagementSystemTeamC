using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Common.Services;
using LearningManagementSystemTeamC.Application.Roles;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Roles;
using LearningManagementSystemTeamC.Domain.UserInfos;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Auth.Commands.RegisterUser;

public class RegisterUserHandler : IRegisterUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserHandler(
        IUserRepository userRepository,
        IUserInfoRepository userInfoRepository,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userInfoRepository = userInfoRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var studentRole = await _roleRepository.GetDefaultRoleAsync(
            cancellationToken)
            ?? throw new NotFoundException(
                RoleRules.RoleNotFoundCode,
                RoleRules.RoleNotFoundMessage);

        var email = StringNormalizer.NormalizeEmail(command.Email);
        var firstName = StringNormalizer.NormalizeName(command.FirstName);
        var lastName = StringNormalizer.NormalizeName(command.LastName);

        if (await _userRepository.GetByEmailAsync(
                email,
                cancellationToken) != null)
        {
            throw new ConflictException(
                UserRules.EmailRegisteredCode,
                UserRules.EmailRegisteredMessage);
        }

        var user = new User(
            email,
            _passwordHasher.Hash(command.Password),
            studentRole.Id);

        var userInfo = new UserInfo(
            user.Id,
            firstName,
            lastName);

        await _userRepository.AddAsync(
            user,
            cancellationToken);

        await _userInfoRepository.AddAsync(
            userInfo,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UserMapper.ToDto(
            user,
            studentRole);
    }
}