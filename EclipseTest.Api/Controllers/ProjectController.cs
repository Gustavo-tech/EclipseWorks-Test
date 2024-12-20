﻿using EclipseTest.Application.Dto.Project;
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
            List<dynamic> projects = new();
            IEnumerable<Project> projectsInDatabase = await _service.GetUserProjectsAsync(userId.Value);

            foreach (Project project in projectsInDatabase)
            {
                projects.Add(new
                {
                    project.Id,
                    project.Title,
                    project.CreatedBy,
                    project.IsEmpty
                });
            }

            return Ok(projects);
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

    [HttpGet("get-report")]
    public async Task<IActionResult> GetProjectReportAsync([FromQuery] int? projectId, [FromQuery] int? userId)
    {
        if (projectId == null)
            return BadRequest("Specify a project to get the report");

        if (userId == null)
            return BadRequest("Specify a user to get the report");

        try
        {
            double average = await _service.GenerateProjectReportAsync(projectId.Value, userId.Value);
            return Ok($"The average of tasks done for this project in the last 30 days is {average}");
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProjectAsync([FromRoute] int id)
    {
        try
        {
            await _service.DeleteProjectAsync(id);
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
