using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces.Repository;

public interface IRepository
{
    T Add<T>(T entity) where T : BaseEntity;

    void Delete<T>(T entity) where T : BaseEntity;

    T? GetById<T>(int id) where T : BaseEntity;

    List<T> List<T>() where T : BaseEntity;

    void Update<T>(T entity) where T : BaseEntity;
}
