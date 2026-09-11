using LearningManagementSystemTeamC.Domain.UserInfos;

namespace LearningManagementSystemTeamC.Application.Users;

public interface IUserInfoRepository
{
    Task<UserInfo?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task AddAsync(UserInfo userInfo, CancellationToken cancellationToken);
    void Update(UserInfo userInfo);
    void Delete(UserInfo userInfo);
    Task<IEnumerable<UserInfo>> GetAllAsync(CancellationToken cancellationToken);
}
