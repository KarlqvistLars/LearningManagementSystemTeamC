namespace LearningManagementSystemTeamC.Api.Common.Contracts;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public ApiErrors? Error { get; set; }

    public static ApiResponse<T> Ok(T data)
    {
        return new ApiResponse<T> { Success = true, Data = data };
    }

    public static ApiResponse<T> Fail(string code, string msg, Dictionary<string, string[]>? details = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Error = new ApiErrors
            {
                Code = code,
                Message = msg,
                Details = details ?? new()
            }
        };
    }
}
