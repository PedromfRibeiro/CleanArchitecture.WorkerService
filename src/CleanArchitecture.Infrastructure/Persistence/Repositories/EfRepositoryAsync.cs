using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories;

public class EfRepositoryAsync(DataContext dbContext) : IRepositoryAsync
{
    public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
    {
        await dbContext.Set<T>().AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async void DeleteAsync<T>(T entity) where T : BaseEntity
    {
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<T?> GetByIdAsync<T>(int id) where T : BaseEntity => await dbContext.Set<T>().SingleOrDefaultAsync<T>(e => e.Id == id);

    public async Task<List<T>> ListAsync<T>() where T : BaseEntity => await dbContext.Set<T>().ToListAsync<T>();

    public async void UpdateAsync<T>(T entity) where T : BaseEntity
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }
}
