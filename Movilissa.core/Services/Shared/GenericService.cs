using System.Linq.Expressions;
using Movilissa_api.Data.IRepositories;
using Movilissa_api.Enums;
using Movilissa.core;
using Movilissa.core.DTOs.Shared;
using Movilissa.core.Interfaces;
using Movilissa.core.Interfaces.IServices;

namespace Movilissa_api.Logic;

public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly IGenericRepository<T> _repository;

    public GenericService(IGenericRepository<T> repository)
    {
        _repository = repository;
    }
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includes)
        {
            return await _repository.GetAll(predicate, includes);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<T?> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<T?> GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            return await _repository.GetById(id, includes);
        }

        public async Task<T> Add(T entity)
        {
            return await _repository.Add(entity);
        }

        public async Task Update(T entity)
        {
            await _repository.Update(entity);
        }

        public async Task Delete(T entity)
        {
            await _repository.Delete(entity);
        }

        public async Task Atomic(Func<Task> operation)
        {
            await _repository.Atomic(operation);
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
