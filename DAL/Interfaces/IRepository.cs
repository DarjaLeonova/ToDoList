using DAL.Entities;

namespace DAL.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    Task AddAsync(TEntity entity);
    
    Task DeleteByIdAsync(int id);

    Task UpdateAsync(TEntity entity);
}