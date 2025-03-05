using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories;

public class EfRepository(DataContext dbContext) : IRepository
{
    public T Add<T>(T entity) where T : BaseEntity
    {
        dbContext.Set<T>().Add(entity);
        dbContext.SaveChanges();

        return entity;
    }

    public void Delete<T>(T entity) where T : BaseEntity
    {
        dbContext.Set<T>().Remove(entity);
        dbContext.SaveChanges();
    }

    public T? GetById<T>(int id) where T : BaseEntity => dbContext.Set<T>().SingleOrDefault(e => e.Id == id);

    public List<T> List<T>() where T : BaseEntity => dbContext.Set<T>().ToList();

    public void Update<T>(T entity) where T : BaseEntity
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        dbContext.SaveChanges();
    }
}
