using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Common.Services;
using LearningManagementSystemTeamC.Application.Roles;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Roles;
using LearningManagementSystemTeamC.Domain.UserInfos;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Users.Commands.UpdateUser;

public class UpdateUserHandler : IUpdateUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(
        IUserRepository userRepository,
        IUserInfoRepository userInfoRepository,
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userInfoRepository = userInfoRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> HandleAsync(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByIdAsync(
            command.UserId,
            cancellationToken)
            ?? throw new NotFoundException(
                UserRules.UserNotFoundCode,
                UserRules.UserNotFoundMessage);

        var email = StringNormalizer.NormalizeEmail(command.Email);

        var emailAlreadyUsed = await _userRepository.GetByEmailAsync(
            email,
            cancellationToken);

        if (emailAlreadyUsed is not null &&
            emailAlreadyUsed.Id != existingUser.Id)
        {
            throw new ConflictException(
                UserRules.EmailRegisteredCode,
                UserRules.EmailRegisteredMessage);
        }

        existingUser.UpdateEmail(email);

        var firstName =
            StringNormalizer.NormalizeName(command.FirstName);

        var lastName =
            StringNormalizer.NormalizeName(command.LastName);

        var existingRole = await _roleRepository.GetActiveByIdAsync(
            existingUser.RoleId,
            cancellationToken)
            ?? throw new NotFoundException(
                RoleRules.RoleNotFoundCode,
                RoleRules.RoleNotFoundMessage);

        var userInfo = await _userInfoRepository.GetByUserIdAsync(
            existingUser.Id,
            cancellationToken);

        if (userInfo is null)
        {
            userInfo = new UserInfo(
                existingUser.Id,
                firstName,
                lastName);

            userInfo.UpdateProfile(
                firstName,
                lastName,
                command.DateOfBirth,
                command.PhoneNumber,
                command.Address,
                command.PostalCode,
                command.City);

            await _userInfoRepository.AddAsync(
                userInfo,
                cancellationToken);
        }
        else
        {
            userInfo.UpdateProfile(
                firstName,
                lastName,
                command.DateOfBirth,
                command.PhoneNumber,
                command.Address,
                command.PostalCode,
                command.City);
        }

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return UserMapper.ToDto(existingUser, existingRole, firstName, lastName);
    }
}