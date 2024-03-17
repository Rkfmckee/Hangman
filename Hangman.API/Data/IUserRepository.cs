using Hangman.API.Models;
using Hangman.Data.Interfaces;

namespace Hangman.API.Data
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(string username);
    }
}
