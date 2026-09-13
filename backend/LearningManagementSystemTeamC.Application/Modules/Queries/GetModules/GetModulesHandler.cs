using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Courses;
using LearningManagementSystemTeamC.Application.Enrollments;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Enrollments;
using LearningManagementSystemTeamC.Domain.Modules;
using LearningManagementSystemTeamC.Domain.Roles;

namespace LearningManagementSystemTeamC.Application.Modules.Queries.GetModules;

public class GetModulesHandler : IGetModulesHandler
{
    private readonly IModuleRepository _moduleRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;

    public GetModulesHandler(
        IModuleRepository moduleRepository,
        ICourseRepository courseRepository,
        IEnrollmentRepository enrollmentRepository)
    {
        _moduleRepository = moduleRepository;
        _courseRepository = courseRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<IReadOnlyList<ModuleDto>> Handle(
        GetModulesQuery query, CancellationToken cancellationToken)
    {
        var checkIfCourseExist = await _courseRepository.GetByIdAsync(query.CourseId, cancellationToken)
        ?? throw new NotFoundException(ModuleRules.ModuleNotFoundCode, ModuleRules.ModuleNotFoundMessage);

        if (query.RoleCode == RoleRules.StudentRoleCode)
        {
            var enrollment = await _enrollmentRepository.GetByUserIdAndCourseIdAsync(
                query.UserId,
                query.CourseId,
                cancellationToken);

            if (enrollment == null)
            {
                throw new UnauthorizedException(EnrollmentRules.UserNotEnrolledCode, EnrollmentRules.UserNotEnrolledMessage);
            }
        }

        var modules = await _moduleRepository.GetModulesByCourseIdAsync(query.CourseId, cancellationToken);

        return modules.Select(module => ModuleMapper.ModuleToDto(module)).ToList();
    }
}