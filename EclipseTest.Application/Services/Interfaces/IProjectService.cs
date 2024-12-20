﻿using EclipseTest.Application.Dto.Project;
using EclipseTest.Domain.Models;

namespace EclipseTest.Application.Services.Interfaces;
public interface IProjectService
{
    Task CreateProjectAsync(CreateProjectDto projectToBeCreated);
    Task<IEnumerable<Project>> GetUserProjectsAsync(int userId);
    Task<double> GenerateProjectReportAsync(int projectId, int userId);
    Task DeleteProjectAsync(int projectId);
}