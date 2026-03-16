using Impacta.GarageTrack.System.Api.Domain.Users.Repositories;
using Microsoft.EntityFrameworkCore;
using UserEntity = Impacta.GarageTrack.System.Api.Domain.Users.Entities.User;

namespace Impacta.GarageTrack.System.Api.Infraestructure.Database.Repositories
{
    public class UserRepository : RepositoryBase<UserEntity, long>, IUserRepository
    {
        public UserRepository(GarageTrackContext context) : base(context)
        {
        }

        public async Task<bool> AnyWithEmailAndCompanyIdAsync(string email, long companyId)
        {
            return await _dbSet.AnyAsync(u => u.Email == email && u.CompanyId == companyId);
        }

        public async Task<List<UserEntity>> GetByEmailAsync(string email)
        {
            return await _dbSet.Where(u => u.Email == email).ToListAsync();
        }
    }
}
