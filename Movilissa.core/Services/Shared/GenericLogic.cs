using Movilissa_api.Data.IRepositories;
using Movilissa.core.Interfaces;

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

    public async Task AddAsync(T entity)
    {
         await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
         await _repository.Update(entity);
    }

    public async Task DeleteAsync(T entity)
    {
         await _repository.Delete(entity);
    }
}
