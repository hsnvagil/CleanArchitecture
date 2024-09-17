using System.Linq.Expressions;
using App.Application.Contracts.Persistence;
using App.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence;

public class GenericRepository<T, TId>(AppDbContext context)
    : IGenericRepository<T, TId> where T : BaseEntity<TId> where TId : struct {
    private readonly DbSet<T> _dbSet = context.Set<T>();
    protected AppDbContext Context = context;

    public Task<List<T>> GetAllAsync() {
        return _dbSet.ToListAsync();
    }

    public Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize) {
        return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) {
        return _dbSet.Where(predicate).AsNoTracking();
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) {
        throw new NotImplementedException();
    }

    public ValueTask<T?> GetByIdAsync(int id) {
        return _dbSet.FindAsync(id);
    }

    public async ValueTask AddAsync(T entity) {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity) {
        _dbSet.Update(entity);
    }

    public void Delete(T entity) {
        _dbSet.Remove(entity);
    }

    public Task<bool> AnyAsync(TId id) {
        return _dbSet.AnyAsync(x => x.Id.Equals(id));
    }

    public IQueryable<T> GetAll() {
        return _dbSet.AsQueryable().AsNoTracking();
    }
}