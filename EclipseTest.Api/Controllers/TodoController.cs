using EclipseTest.Application.Dto.Todo;
using EclipseTest.Application.Services.Interfaces;
using EclipseTest.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EclipseTest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly ITodoService _service;

    public TodoController(ITodoService todoService)
    {
        _service = todoService ?? throw new ArgumentNullException(nameof(todoService));
    }

    [HttpGet]
    public async Task<IActionResult> GetProjectTodosAsync([FromQuery] int projectId)
    {
        try
        {
            IEnumerable<Todo> todos = await _service.GetAllProjectTodosAsync(projectId);
            return Ok(todos);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodoAsync([FromBody] CreateTodoDto dto)
    {
        try
        {
            await _service.AddTodoToProjectAsync(dto);
            return StatusCode(201);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTodoAsync([FromBody] UpdateTodoDto dto)
    {
        try
        {
            await _service.UpdateTodoFromProjectAsync(dto);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoAsync([FromRoute] int id)
    {
        try
        {
            await _service.RemoveTodoFromProjectAsync(id);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}
