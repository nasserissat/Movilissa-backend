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
    public static Task<List<Item>> GetGenericStatus() => Task.FromResult(GetListFromEnum<GenericStatus>());
    
    private static List<Item> GetListFromEnum<T>() where T : Enum
    {
        var list = new List<Item>();
        foreach (var value in Enum.GetValues(typeof(T)))
            list.Add(new Item((int)(object)value, value.ToString().PascalCaseWithInitialsToTitleCase()));
        return list;
    }

}
