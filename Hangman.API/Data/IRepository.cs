namespace Hangman.Data.Interfaces
{
	public interface IRepository<T>
    {
        #region Get methods

        IEnumerable<T> GetAll();
        T Get(Guid id);

        #endregion

        #region CRUD methods

        bool Add(T item);
        bool Update(T item);
        bool Delete(T item);
        bool Save();

        #endregion
    }
}
