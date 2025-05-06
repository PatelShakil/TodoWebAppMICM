namespace TodoWebApp.Services.Impl
{
    // Services/Implementations/TextFileTodoService.cs
    using System.Text.Json;
    using TodoWebApp.Models;

    public class TextFileTodoService : ITodoService
    {
        private readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "todos.txt");

        public async Task<List<Todo>> GetTodosAsync()
        {
            if (!File.Exists(filePath)) return new List<Todo>();
            var content = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Todo>>(content) ?? new List<Todo>();
        }

        public async Task AddTodoAsync(Todo todo)
        {
            var todos = await GetTodosAsync();
            todos.Add(todo);
            await SaveTodosAsync(todos);
        }

        public async Task DeleteTodoAsync(Guid id)
        {
            var todos = await GetTodosAsync();
            todos.RemoveAll(t => t.Id == id);
            await SaveTodosAsync(todos);
        }

        public async Task ToggleTodoAsync(Guid id)
        {
            var todos = await GetTodosAsync();
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                todo.IsCompleted = !todo.IsCompleted;
                await SaveTodosAsync(todos);
            }
        }

        private async Task SaveTodosAsync(List<Todo> todos)
        {
            var json = JsonSerializer.Serialize(todos, new JsonSerializerOptions { WriteIndented = true });
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            await File.WriteAllTextAsync(filePath, json);
        }
    }

}
