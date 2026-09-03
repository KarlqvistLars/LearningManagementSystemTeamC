using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Common.Services;
using LearningManagementSystemTeamC.Application.Roles;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Roles;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;

public class CreateUserHandler : ICreateUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IRoleRepository roleRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
    }
    public async Task<UserDto> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var existingRole = await _roleRepository.GetActiveById(command.RoleId) ?? throw new NotFoundException(
                RoleRules.RoleNotFoundCode,
                RoleRules.RoleNotFoundMessage);

        if (await _userRepository.GetByEmail(command.Email) != null)
            throw new ConflictException(
                UserRules.EmailRegisteredCode,
                UserRules.EmailRegisteredMessage);

        var passwordHash = _passwordHasher.Hash(command.Password);
        var normalizedEmail = StringNormalizer.NormalizeEmail(command.Email);

        var user = new User(
            normalizedEmail,
            passwordHash,
            existingRole.Id);

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return UserMapper.ToDto(user, existingRole);
    }
}
