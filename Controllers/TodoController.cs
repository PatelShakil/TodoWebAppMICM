using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoWebApp.Models;
using TodoWebApp.Services;
using TodoWebApp.Services.Impl;

namespace TodoWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public TodoController(IServiceProvider provider)
        {
            _provider = provider;
        }

        private ITodoService GetService(string storageType)=> storageType switch
        {
            "SqlServer" => _provider.GetRequiredService<SqlTodoService>(),
            _ => _provider.GetRequiredService<TextFileTodoService>()
        };

    [HttpGet]
        public async Task<IActionResult> GetTodos([FromQuery] string storageType)
        {
            var service = GetService(storageType);
            var todos = await service.GetTodosAsync();
            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo([FromQuery] string storageType, [FromBody] Todo todo)
        {
            var service = GetService(storageType);
            await service.AddTodoAsync(todo);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id, [FromQuery] string storageType)
        {
            var service = GetService(storageType);
            await service.DeleteTodoAsync(id);
            return Ok();
        }

        [HttpPut("toggle/{id}")]
        public async Task<IActionResult> ToggleTodo(Guid id, [FromQuery] string storageType)
        {
            var service = GetService(storageType);
            await service.ToggleTodoAsync(id);
            return Ok();
        }
    }

}
