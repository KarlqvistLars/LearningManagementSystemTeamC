using LearningManagementSystemTeamC.Application.Auth;
using LearningManagementSystemTeamC.Application.Auth.Commands.Login;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Roles;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.Roles;
using LearningManagementSystemTeamC.Domain.Users;
using Microsoft.Extensions.Options;
using Moq;

namespace LearningManagementSystemTeamC.UnitTests.Auth;

public class LoginHandlerTests
{
    [Fact]
    public async Task HandleAsync_ValidCredentials_ReturnsLoginResult()
    {
        // Arrange
        var userRepository = new Mock<IUserRepository>();
        var roleRepository = new Mock<IRoleRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var jwtTokenService = new Mock<IJwtTokenService>();

        var jwtSettings = Options.Create(new JwtSettings
        {
            ExpiresInMinutes = 60
        });

        var testEmail = "test@lms.com";
        var testPass = "testpass";
        var testHashedPass = "hashed-password";
        var testToken = "test-jwt-token";

        var studentRole = new Role(
            RoleRules.StudentRoleName,
            RoleRules.StudentRoleCode);

        var user = new User(
            testEmail,
            testHashedPass,
            studentRole.Id);

        userRepository
            .Setup(repository => repository.GetByEmailAsync(
                testEmail,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        passwordHasher
            .Setup(hasher => hasher.Verify(
                testPass,
                testHashedPass))
            .Returns(true);

        roleRepository
            .Setup(repository => repository.GetByIdAsync(
                user.RoleId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(studentRole);

        jwtTokenService
            .Setup(service => service.CreateToken(
                user,
                studentRole))
            .Returns(testToken);

        var handler = new LoginHandler(
            userRepository.Object,
            roleRepository.Object,
            jwtTokenService.Object,
            jwtSettings,
            passwordHasher.Object);

        var command = new LoginCommand(
            testEmail,
            testPass);

        // Act
        var result = await handler.HandleAsync(
            command,
            CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testToken, result.AccessToken);
        Assert.Equal(60, result.ExpiresInMinutes);
        Assert.Equal(testEmail, result.User.Email);
        Assert.Equal(
            RoleRules.StudentRoleName,
            result.User.RoleName);

        jwtTokenService.Verify(
            service => service.CreateToken(
                user,
                studentRole),
            Times.Once);
    }
}