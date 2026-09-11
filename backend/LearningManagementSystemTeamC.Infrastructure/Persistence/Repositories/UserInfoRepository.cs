using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.UserInfos;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class UserInfoRepository : IUserInfoRepository
{
    private readonly ApplicationDbContext _context;

    public UserInfoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserInfo?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _context.UserInfos
            .FirstOrDefaultAsync(
                x => x.UserId == userId,
                cancellationToken);
    }

    public async Task AddAsync(
        UserInfo userInfo,
        CancellationToken cancellationToken)
    {
        await _context.UserInfos.AddAsync(
            userInfo,
            cancellationToken);
    }

    public void Update(UserInfo userInfo)
    {
        _context.UserInfos.Update(userInfo);
    }

    public void Delete(UserInfo userInfo)
    {
        _context.UserInfos.Remove(userInfo);
    }

    public async Task<IEnumerable<UserInfo>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.UserInfos.ToListAsync(cancellationToken);
    }
}