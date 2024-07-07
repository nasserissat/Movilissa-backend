using Movilissa_api.Data.IRepositories;
using Movilissa_api.Enums;
using Movilissa.core;
using Movilissa.core.DTOs.Shared;
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
    public static Task<List<Item>> GetGenericStatus() => Task.FromResult(GetListFromEnum<GenericStatus>());
    
    private static List<Item> GetListFromEnum<T>() where T : Enum
    {
        var list = new List<Item>();
        foreach (var value in Enum.GetValues(typeof(T)))
            list.Add(new Item((int)(object)value, value.ToString().PascalCaseWithInitialsToTitleCase()));
        return list;
    }

}
