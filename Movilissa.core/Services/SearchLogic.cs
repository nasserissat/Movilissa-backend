using Movilissa_api.Data.IRepositories;
using Movilissa_api.Models;

namespace Movilissa_api.Logic;

public class SearchLogic
{
    private readonly ISearchRepository _searchRepository;

    public SearchLogic(ISearchRepository searchRepository)
    {
        _searchRepository = searchRepository;
    }
    // public async Task<IEnumerable<Schedule>> SearchSchedulesAsync(int origin, int destination, DateTime? date, int company)
    // {
    //     var schedules = await _searchRepository.SearchSchedulesAsync(origin,  destination, date, company);
    //     var filteredSchedules = schedules.AsQueryable();
    //     return filteredSchedules.ToList();
    // }
}