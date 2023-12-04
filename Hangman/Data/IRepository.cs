namespace Hangman.Data.Interfaces
{
	public interface IRepository<T>
    {
        #region Get methods

        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Get(int id);
        Task<T> GetAsync(int id);

        #endregion

        #region CRUD methods

        bool Add(T item);
        bool Update(T item);
        bool Delete(T item);
        bool Save();

        #endregion
    }
}
