using EclipseTest.Application.Dto.Project;
using EclipseTest.Application.Dto.Todo;
using EclipseTest.Application.Services.Interfaces;
using EclipseTest.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EclipseTest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _service;

    public ProjectController(IProjectService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    public async Task<IActionResult> GetUserProjectsAsync([FromQuery] int? userId)
    {
        if (userId == null)
            return BadRequest("Specify a user to get the projects");

		try
		{
            IEnumerable<Project> projects = await _service.GetUserProjectsAsync(userId.Value);
            return Ok(projects);
		}
		catch (Exception)
		{
            return StatusCode(500);
		}
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProjectAsync([FromBody] CreateProjectDto dto)
    {
        try
        {
            await _service.CreateProjectAsync(dto);
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

    [HttpPost("insert-task")]
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
}
