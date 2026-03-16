using Impacta.GarageTrack.System.Api.Domain.Kernel;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GarageTrack.System.Api.Infraestructure.Database.Repositories
{
    public abstract class RepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId>
        where TEntity : EntityBase<TId>
    {
        protected readonly GarageTrackContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected RepositoryBase(GarageTrackContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}
