namespace Core.Interfasces
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenrericRepo<T> Entity { get; }
        void save();
    }
}
