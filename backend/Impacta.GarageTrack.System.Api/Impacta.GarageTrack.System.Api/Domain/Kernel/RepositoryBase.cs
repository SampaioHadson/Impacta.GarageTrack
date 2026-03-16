namespace Impacta.GarageTrack.System.Api.Domain.Kernel
{
    public interface IRepositoryBase<TEntity, TId> where TEntity : EntityBase<TId>
    {
        Task<TEntity> GetByIdAsync(TId id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TId id);
    }
}
