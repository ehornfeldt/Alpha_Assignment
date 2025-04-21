using Business.Models;
using Data.Entities;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;

namespace Business.Services;

public interface IProjectService
{
    Task<ProjectResult> CreateProjectsAsync(AddProjectFormData formData);
    Task<ProjectResult<Project>> GetProjectAsync(string id);
    Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync();
}

public class ProjectService(IProjectRepository projectRepository, IStatusService statusService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService = statusService;

    public async Task<ProjectResult> CreateProjectsAsync(AddProjectFormData formData)
    {
        if (formData == null)
        {
            return new ProjectResult { Succeeded = false, StatusCode = 400, Error = "Not all required field are supplied." };
        }
        var projectEntity = formData.MapTo<ProjectEntity>();
        var statusResult = await _statusService.GetStatusByIdAsync(1);
        var status = statusResult.Result;

        projectEntity.StatusId = status!.Id;

        var response = await _projectRepository.AddAsync(projectEntity);

        return response.Succeeded
           ? new ProjectResult { Succeeded = true, StatusCode = 201 }
           : new ProjectResult { Succeeded = false, StatusCode = response.StatusCode, Error = response.Error };
    }

    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync()
    {
        var response = await _projectRepository.GetAllAsync
            (orderByDescending: true,
                sortBy: s => s.CreatedAt,
                where: null,
                include => include.User,
                include => include.Status,
                include => include.Client
            );

        return new ProjectResult<IEnumerable<Project>> { Succeeded = true, StatusCode = 200, Result = response.Result };

    }

    public async Task<ProjectResult<Project>> GetProjectAsync(string id)
    {
        var response = await _projectRepository.GetAsync
            (where: x => x.Id == id,
                include => include.User,
                include => include.Status,
                include => include.Client
            );

        return response.Succeeded
            ? new ProjectResult<Project> { Succeeded = true, StatusCode = 200, Result = response.Result }
            : new ProjectResult<Project> { Succeeded = false, StatusCode = 404, Error = $"Project with id: {id} was not found" };

    }

    //public async Task<ProjectResult<bool>> UpdateProjectAsync(string id, UpdateProjectFormData formData)
    //{
    //    if (formData == null)
    //    {
    //        return new ProjectResult<bool> { Succeeded = false, StatusCode = 400, Error = "Not all required field are supplied." };
    //    }
    //    var projectEntity = formData.MapTo<ProjectEntity>();
    //    var statusResult = await _statusService.GetStatusByIdAsync(1);
    //    var status = statusResult.Result;
    //    projectEntity.StatusId = status!.Id;
    //    projectEntity.Id = id;
    //    var response = await _projectRepository.UpdateAsync(projectEntity);
    //    return response.Succeeded
    //        ? new ProjectResult<bool> { Succeeded = true, StatusCode = 200 }
    //        : new ProjectResult<bool> { Succeeded = false, StatusCode = response.StatusCode, Error = response.Error };
    //}

    //public async Task<ProjectResult<bool>> DeleteProjectAsync(string id)
    //{
    //    var projectEntity = await _projectRepository.GetAsync(x => x.Id == id);
    //    if (projectEntity == null)
    //    {
    //        return new ProjectResult<bool> { Succeeded = false, StatusCode = 404, Error = $"Project with id: {id} was not found" };
    //    }
    //    var response = await _projectRepository.DeleteAsync(projectEntity.Result);
    //    return response.Succeeded
    //        ? new ProjectResult<bool> { Succeeded = true, StatusCode = 200 }
    //        : new ProjectResult<bool> { Succeeded = false, StatusCode = response.StatusCode, Error = response.Error };
    //}
}
