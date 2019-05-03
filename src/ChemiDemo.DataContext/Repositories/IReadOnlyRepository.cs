namespace ChemiDemo.DataContext.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Entities;

    public interface IReadOnlyRepository
    {
        T Get<T>(object id) where T : IEntity;

        IQueryable<T> Query<T>() where T : IEntity;

        IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate) where T : IEntity;        
    }
}