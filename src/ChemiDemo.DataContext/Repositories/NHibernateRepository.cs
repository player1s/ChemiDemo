namespace ChemiDemo.DataContext.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Entities;
    using NHibernate;

    public class NHibernateRepository
        : IRepository
    {
        public NHibernateRepository(ISession session) {
            this.Session = session;
        }

        protected virtual ISession Session { get; }

        public void Delete<T>(T item) where T : IEntity
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                try
                {
                    Session.Delete(item);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Failed Delete on item of type {typeof(T)}", ex);
                }
            }
        }

        public void Evict(IEntity obj) => Session.Evict(obj);

        public void Evict<T>() where T : IEntity => Session.SessionFactory.Evict(typeof(T));

        public T Get<T>(object id) where T : IEntity => Session.Get<T>(id);

        public object GetIdentifier(object obj) => Session.GetIdentifier(obj);

        public T Proxy<T>(object id) where T : IEntity => Session.Load<T>(id);

        public IQueryable<T> Query<T>() where T : IEntity => Session.Query<T>();

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate) where T : IEntity => Query<T>().Where(predicate);

        public T SaveOrUpdate<T>(T item) where T : IEntity
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                try
                {
                    Session.SaveOrUpdate(item);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Failed SaveOrUpdate on item of type {typeof(T)}", ex);
                }
            }

            return item;
        }
    }
}