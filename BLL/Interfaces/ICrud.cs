namespace BLL.Interfaces;

public interface ICrud<TModel> where TModel : class
{
    Task<IEnumerable<TModel>> GetAllAsync();
    
    Task AddAsync(TModel model);

    Task UpdateAsync(TModel model);

    Task DeleteByIdAsync(int modelId);
}