using TodoApp.Models;
using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITodoRepository
    {
        Task InsertTodoAsync(Todo todo);
        Task<Todo> SelectTodoAsync(long todoId);
        Task<List<Todo>> SelectAllTodosAsync();
        Task<bool> DeleteTodoAsync(int id);
        Task<bool> UpdateTodoAsync(Todo todo);
    }
}
