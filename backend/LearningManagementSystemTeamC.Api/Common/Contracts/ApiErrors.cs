namespace LearningManagementSystemTeamC.Api.Common.Contracts;

public class ApiErrors
{
    public string Code { get; set; } = "";
    public string Message { get; set; } = "";
    public Dictionary<string, string[]> Details { get; set; } = new();
}
