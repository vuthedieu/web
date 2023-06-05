namespace BTLWeb.Repository
{
    public interface IRepository<T>
    {
        T Add(T entity);
    }
}