using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Application.Auth.Commands.ResetPassword;

public class ResetPasswordHandler : IResetPasswordHandler
{
    private readonly IPasswordResetTokenRepository _tokenRepository;
    private readonly IPasswordResetTokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordHandler(
        IPasswordResetTokenRepository tokenRepository,
        IPasswordResetTokenService tokenService,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _tokenRepository = tokenRepository;
        _tokenService = tokenService;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(
        ResetPasswordCommand command,
        CancellationToken cancellationToken)
    {
        var tokenHash = _tokenService.HashToken(command.Token);

        var existingResetToken = await _tokenRepository.GetByTokenHashAsync(
            tokenHash,
            cancellationToken);

        if (existingResetToken is null || !existingResetToken.IsValid())
            throw new DomainException(ResetPasswordRules.TokenInvalidCode, ResetPasswordRules.TokenInvalidMessage);

        var existingUser = await _userRepository.GetByIdAsync(
            existingResetToken.UserId,
            cancellationToken) ?? throw new DomainException(ResetPasswordRules.TokenInvalidCode, ResetPasswordRules.TokenInvalidMessage);

        var passwordHash = _passwordHasher.Hash(command.NewPassword);

        existingUser.ChangePassword(passwordHash);

        existingResetToken.MarkAsUsed();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}