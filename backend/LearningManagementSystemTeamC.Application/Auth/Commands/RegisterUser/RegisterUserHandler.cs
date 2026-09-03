using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Common.Services;
using LearningManagementSystemTeamC.Application.Roles;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Roles;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Auth.Commands.RegisterUser;

public class RegisterUserHandler : IRegisterUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IRoleRepository roleRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
    }
    public async Task<UserDto> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var existingRole = await _roleRepository.GetDefaultRoleAsync(cancellationToken) ?? throw new NotFoundException(
            RoleRules.RoleNotFoundCode,
            RoleRules.RoleNotFoundMessage);

        if (await _userRepository.GetByEmailAsync(command.Email, cancellationToken) != null)
            throw new ConflictException(
                UserRules.EmailRegisteredCode,
                UserRules.EmailRegisteredMessage);

        var user = new User(
            StringNormalizer.NormalizeEmail(command.Email),
            _passwordHasher.Hash(command.Password),
            existingRole.Id);

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return UserMapper.ToDto(user, existingRole);
    }
}
