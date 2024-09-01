using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface IUserRepository
    {
        Task<int> RegisterUserAsync(User user);
    }
}
