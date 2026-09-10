using LearningManagementSystemTeamC.Application.Users;
using LearningManagementSystemTeamC.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetUsersByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        return await _context.Users.Where(u => ids.Contains(u.Id)).ToListAsync(cancellationToken);
    }
}
