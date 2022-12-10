﻿using ComprasSolution.Domain.Entities;

namespace ComprasSolution.Domain.Interfaces
{
    public interface ICrudRepository<T> where T : Base
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
