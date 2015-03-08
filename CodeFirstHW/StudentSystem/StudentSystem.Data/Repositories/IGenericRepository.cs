﻿namespace StudentSystem.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> All(Expression<Func<T, bool>> conditions);

        T Add(T entity);

        T Update(T entity);

        T Delete(T entity);

        void Detach(T entity);

        DbEntityEntry Attach(T entity, EntityState newEntityState);
    }
}
