using Innoloft_Test.Models;

namespace Innoloft_Test.Interfaces
{
    public interface IUser
    {
        Task<User?> GetUser(int id);
    }
}
