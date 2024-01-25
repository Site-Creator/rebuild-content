namespace RebuilderLib.Services.Repositories.Data
{
    public interface IDataRepository<T> where T : class
    {
        public Task<List<T>> FetchAll();

        public Task<T> FetchSingle(string Id);

        public Task Save(List<T> articles);

        public Task Save(T article);
    }
}
