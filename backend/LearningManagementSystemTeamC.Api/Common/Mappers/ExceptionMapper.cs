using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Api.Common.Mappers
{
    public static class ExceptionMapper
    {
        public static (string Code, string Message, int StatusCode, Dictionary<string, string[]>? Details)
            Map(Exception ex)
        {
            return ex switch
            {
                DomainException e => (
                    e.Code,
                    e.Message,
                    StatusCodes.Status400BadRequest,
                    null
                ),
                NotFoundException e => (
                    e.Code,
                    e.Message,
                    StatusCodes.Status404NotFound,
                    null
                ),
                ConflictException e => (
                    e.Code,
                    e.Message,
                    StatusCodes.Status409Conflict,
                    null
                ),
                _ => (
                    ExceptionConstants.DefaultExceptionCode,
                    ExceptionConstants.DefaultExceptionMessage,
                    StatusCodes.Status500InternalServerError,
                    null
                )
            };
        }
    }
}
