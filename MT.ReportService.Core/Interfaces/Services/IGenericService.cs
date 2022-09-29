﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MT.ReportService.Core.Interfaces.Services
{
    public interface IGenericService<TEntity> where TEntity : class, new()
    {
        Task<TEntity> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> AddAsync(TEntity entity);
        void Delete(TEntity entity);
        TEntity Update(TEntity entity);
    }
}
