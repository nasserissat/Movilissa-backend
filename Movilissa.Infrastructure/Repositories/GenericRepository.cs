using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Movilissa_api.Data.Context;
using Movilissa.core.Interfaces;

namespace Movilissa.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
   
    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (includes.Length != 0)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await query.ToListAsync();
    }
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        var list = await _dbSet.ToListAsync();
        return list.AsReadOnly();
    }
    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        var query = includes.Aggregate(_dbSet.AsQueryable(), (current, include) => current.Include(include));
        var entity = await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        return entity;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity;
    }

    public async Task<T> AddAsync(T entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity; 
    }
    
    public async Task Update(T data)
    {
       _dbSet.Update(data);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(T data)
    {
        _dbSet.Remove(data);
        await _context.SaveChangesAsync();
    }
    
    
    public async Task Atomic(Func<Task> operation)
    {

        if (_context.Database.CurrentTransaction != null)
        {
            await operation();
            return;
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await operation();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("Exception in atomic data operation.", ex);
        }
    }
}