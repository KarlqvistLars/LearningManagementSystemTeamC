namespace LearningManagementSystemTeamC.Domain.Common.Exceptions;

public class NotFoundException : Exception
{
    public string Code { get; }

    public NotFoundException(string code, string message)
        : base(message)
    {
        Code = code;
    }
}
