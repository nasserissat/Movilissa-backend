using Microsoft.EntityFrameworkCore;
using Movilissa_api.Data.Context;
using Movilissa_api.Models;

namespace Movilissa_api.Data.Repositories;

public class SearchRepository
{
    private readonly ApplicationDbContext _context;
    public SearchRepository (ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Schedule>> SearchSchedulesAsync(int origin, int destination, DateTime? date, int company)
    {
        return await _context.Schedules.ToListAsync();

    }

}