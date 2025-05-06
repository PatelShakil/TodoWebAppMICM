using TodoWebApp.Models;

namespace TodoWebApp.Services
{
    // Services/Interfaces/ITodoService.cs
    public interface ITodoService
    {
        Task<List<Todo>> GetTodosAsync();
        Task AddTodoAsync(Todo todo);
        Task DeleteTodoAsync(Guid id);
        Task ToggleTodoAsync(Guid id);
    }

}
