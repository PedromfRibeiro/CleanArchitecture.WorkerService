using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces.Repository;

public interface IRepositoryAsync
{
    Task<T> AddAsync<T>(T entity) where T : BaseEntity;

    void DeleteAsync<T>(T entity) where T : BaseEntity;

    Task<T?> GetByIdAsync<T>(int id) where T : BaseEntity;

    Task<List<T>> ListAsync<T>() where T : BaseEntity;

    void UpdateAsync<T>(T entity) where T : BaseEntity;
}
