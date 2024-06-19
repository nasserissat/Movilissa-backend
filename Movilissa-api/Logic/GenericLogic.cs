using Movilissa_api.Data.IRepositories;

namespace Movilissa_api.Logic;

public class GenericLogic<T> where T : class
{
    private readonly IGenericRepository<T> _repository;

    public GenericLogic(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        return await _repository.UpdateAsync(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
