using LearningManagementSystemTeamC.Application.Auth.Commands.RegisterUser;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Roles;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Roles;
using LearningManagementSystemTeamC.Domain.Users;
using Moq;

namespace LearningManagementSystemTeamC.UnitTests.Auth;

public class RegisterUserHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    [Fact]
    public async Task Handle_ValidCommand_CreatesStudent()
    {
        // Arrange
        var testFirstName = "Test";
        var testLastName = "User";
        var userInfoRepository = new Mock<IUserInfoRepository>();
        var userRepository = new Mock<IUserRepository>();
        var roleRepository = new Mock<IRoleRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var studentRole = new Role(
            RoleRules.StudentRoleName,
            RoleRules.StudentRoleCode);

        var testEmail = "test@lms.com";
        var testPass = "testpass";
        var testHashedPass = "hashed-password";

        roleRepository
            .Setup(repository => repository.GetDefaultRoleAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(studentRole);

        userRepository
            .Setup(repository => repository.GetByEmailAsync(
                testEmail,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        passwordHasher
            .Setup(hasher => hasher.Hash(testPass))
            .Returns(testHashedPass);

        var handler = new RegisterUserHandler(
            userRepository.Object,
            userInfoRepository.Object,
            roleRepository.Object,
            passwordHasher.Object,
            unitOfWork.Object);

        var command = new RegisterUserCommand(
            testEmail,
            testPass,
            testFirstName,
            testLastName);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testEmail, result.Email);
        Assert.Equal(RoleRules.StudentRoleName, result.RoleName);

        userRepository.Verify(
            repository => repository.AddAsync(
                It.IsAny<User>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        unitOfWork.Verify(
            unitOfWork => unitOfWork.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ExistingEmail_ThrowsConflictException()
    {
        // Arrange
        var testFirstName = "Test";
        var testLastName = "User";
        var userInfoRepository = new Mock<IUserInfoRepository>();
        var userRepository = new Mock<IUserRepository>();
        var roleRepository = new Mock<IRoleRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var testEmail = "test@lms.com";
        var testPass = "testpass";
        var testHashedPass = "hashed-password";

        var studentRole = new Role(
            RoleRules.StudentRoleName,
            RoleRules.StudentRoleCode);

        roleRepository
            .Setup(repository => repository.GetDefaultRoleAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(studentRole);

        var existingUser = new User(
            testEmail,
            testHashedPass,
            Guid.NewGuid());

        userRepository
            .Setup(repository => repository.GetByEmailAsync(
                testEmail,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);

        var handler = new RegisterUserHandler(
            userRepository.Object,
            userInfoRepository.Object,
            roleRepository.Object,
            passwordHasher.Object,
            unitOfWork.Object);

        var command = new RegisterUserCommand(
            testEmail,
            testPass,
            testFirstName,
            testLastName);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(() =>
            handler.Handle(
                command,
                CancellationToken.None));

        userRepository.Verify(
            repository => repository.AddAsync(
                It.IsAny<User>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        unitOfWork.Verify(
            unitOfWork => unitOfWork.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_DefaultRoleDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var testFirstName = "Test";
        var testLastName = "User";
        var userInfoRepository = new Mock<IUserInfoRepository>();
        var userRepository = new Mock<IUserRepository>();
        var roleRepository = new Mock<IRoleRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var testEmail = "test@lms.com";
        var testPass = "testpass";

        roleRepository
            .Setup(repository => repository.GetDefaultRoleAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Role?)null);

        var handler = new RegisterUserHandler(
            userRepository.Object,
            userInfoRepository.Object,
            roleRepository.Object,
            passwordHasher.Object,
            unitOfWork.Object);

        var command = new RegisterUserCommand(
            testEmail,
            testPass,
            testFirstName,
            testLastName);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(
                command,
                CancellationToken.None));

        userRepository.Verify(
            repository => repository.AddAsync(
                It.IsAny<User>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        unitOfWork.Verify(
            unitOfWork => unitOfWork.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

}