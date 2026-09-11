using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Common.Services;
using LearningManagementSystemTeamC.Application.Roles;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Roles;
using LearningManagementSystemTeamC.Domain.UserInfos;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;

public class CreateUserHandler : ICreateUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(
        IUserRepository userRepository,
        IUserInfoRepository userInfoRepository,
        IUnitOfWork unitOfWork,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _userInfoRepository = userInfoRepository;
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> HandleAsync(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var existingRole = await _roleRepository.GetActiveByIdAsync(
            command.RoleId,
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
            existingRole.Id);

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
            existingRole,
            userInfo);
    }
}