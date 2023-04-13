using System.Linq.Expressions;

namespace FreelanceNFControl.Core.Interfaces
{
    public interface IService<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> Add(TEntity entity);
        Task AddRange(List<TEntity> entities);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task Update(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task Remove(TEntity entity);
        Task RemoveRangeAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
