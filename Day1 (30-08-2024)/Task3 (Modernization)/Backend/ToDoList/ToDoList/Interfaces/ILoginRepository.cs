using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ILoginRepository
    {
        Task<bool> ValidateAsync(LoginDto loginDto);
    }
}
