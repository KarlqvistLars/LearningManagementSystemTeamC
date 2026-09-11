using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.PasswordResetTokens;

namespace LearningManagementSystemTeamC.Application.Auth.Commands.ForgotPassword;

public class ForgotPasswordHandler : IForgotPasswordHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IPasswordResetTokenRepository _tokenRepository;
    private readonly IPasswordResetTokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public ForgotPasswordHandler(
        IUserRepository userRepository,
        IUserInfoRepository userInfoRepository,
        IPasswordResetTokenRepository tokenRepository,
        IPasswordResetTokenService tokenService,
        IEmailService emailService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userInfoRepository = userInfoRepository;
        _tokenRepository = tokenRepository;
        _tokenService = tokenService;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(
        ForgotPasswordCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(
            command.Email,
            cancellationToken);

        if (user is null)
            return;

        var userInfo = await _userInfoRepository.GetByUserIdAsync(
            user.Id,
            cancellationToken);

        if (userInfo is null)
            return;

        var token = _tokenService.GenerateToken();
        var tokenHash = _tokenService.HashToken(token);

        var expiresAt = DateTime.UtcNow.AddMinutes(30);

        var resetToken = new PasswordResetToken(
            user.Id,
            tokenHash,
            expiresAt);

        await _tokenRepository.AddAsync(
            resetToken,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var resetLink =
            $"http://localhost:5173/reset-password?token={Uri.EscapeDataString(token)}";

        await _emailService.SendPasswordResetAsync(
            user.Email,
            userInfo.FirstName,
            resetLink,
            cancellationToken);
    }
}