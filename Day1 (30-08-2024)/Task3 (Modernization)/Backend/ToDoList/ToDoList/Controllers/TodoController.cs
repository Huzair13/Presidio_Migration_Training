using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using ToDoList.Interfaces;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyCors")]

    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoService;

        public TodoController(ITodoRepository todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var todos = await _todoService.SelectAllTodosAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var todo = await _todoService.SelectTodoAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] Todo todo)
        {
            if (todo == null)
            {
                return BadRequest("Todo cannot be null.");
            }

            await _todoService.InsertTodoAsync(todo);
            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateTodo(int id, [FromBody] Todo todo)
        {
            if (todo == null || id != todo.Id)
            {
                return BadRequest("Todo ID mismatch.");
            }

            var result = await _todoService.UpdateTodoAsync(todo);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            await _todoService.DeleteTodoAsync(id);
            return NoContent();
        }
    }
}
