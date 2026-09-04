namespace LearningManagementSystemTeamC.Domain.Common.Exceptions;

public class ConflictException : Exception
{
    public string Code { get; }

    public ConflictException(string code, string message)
        : base(message)
    {
        Code = code;
    }
}
