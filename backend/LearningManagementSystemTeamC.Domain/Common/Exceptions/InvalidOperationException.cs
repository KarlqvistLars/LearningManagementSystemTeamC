namespace LearningManagementSystemTeamC.Domain.Common.Exceptions;

public class InvalidOperationException : Exception
{
    public string Code { get; }

    public InvalidOperationException(string code, string message)
        : base(message)
    {
        Code = code;
    }
}
