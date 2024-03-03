using Hangman.API.Models;
using Hangman.Data;
using Hangman.Data.Repositories;

namespace Hangman.API.Data
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public virtual User Get(string username)
        {
            return dbSet.FirstOrDefault(u => u.Username == username);

        }
    }
}
