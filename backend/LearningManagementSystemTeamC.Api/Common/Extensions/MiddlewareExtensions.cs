using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Common.Middlewares;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace LearningManagementSystemTeamC.Api.Common.Extensions;

public static class MiddlewareExtensions
{
    public static WebApplication UseApiMiddlewares(
        this WebApplication app)
    {
        app.UseStatusCodePages(async context =>
        {
            var response = context.HttpContext.Response;

            var (code, message) = response.StatusCode switch
            {
                StatusCodes.Status401Unauthorized => (
                    ExceptionConstants.UnauthorizedCode,
                    ExceptionConstants.UnauthorizedMessage),

                StatusCodes.Status403Forbidden => (
                    ExceptionConstants.ForbiddenCode,
                    ExceptionConstants.ForbiddenMessage),

                StatusCodes.Status404NotFound => (
                    ExceptionConstants.NotFoundCode,
                    ExceptionConstants.NotFoundMessage),

                StatusCodes.Status405MethodNotAllowed => (
                    ExceptionConstants.MethodNotAllowedCode,
                    ExceptionConstants.MethodNotAllowedMessage),

                _ => (
                    ExceptionConstants.HttpErrorCode,
                    ExceptionConstants.HttpErrorMessage)
            };

            response.ContentType = "application/json";

            await response.WriteAsJsonAsync(
                ApiResponse<Dictionary<string, string[]>>.Fail(
                    code,
                    message,
                    null));
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        return app;
    }
}