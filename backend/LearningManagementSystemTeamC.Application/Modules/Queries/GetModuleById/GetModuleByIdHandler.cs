using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Mappers;

namespace LearningManagementSystemTeamC.Application.Modules.Queries.GetModuleById;

public class GetModuleByIdHandler : IGetModuleByIdHandler
{
    private readonly IModuleRepository _moduleRepository;

    public GetModuleByIdHandler(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    public async Task<ModuleDto?> Handle(GetModuleByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _moduleRepository.GetByIdAsync(query.Id, cancellationToken);
        return result == null ? null : ModuleMapper.ModuleToDto(result);
    }
}