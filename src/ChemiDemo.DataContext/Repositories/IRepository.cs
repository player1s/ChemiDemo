namespace ChemiDemo.DataContext.Repositories
{
    using Entities;

    public interface IRepository
        : IReadOnlyRepository
    {
        void Delete<T>(T item) where T : IEntity;

        T SaveOrUpdate<T>(T item) where T : IEntity;

        T Proxy<T>(object id) where T : IEntity;

        object GetIdentifier(object obj);

        void Evict(IEntity obj);

        void Evict<T>() where T : IEntity;
    }
}