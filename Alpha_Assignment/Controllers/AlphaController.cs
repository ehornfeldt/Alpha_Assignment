using Business.Services;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Models;

namespace Presentation.Controllers;
public class AlphaController(IProjectService projectService) : Controller
{
    private readonly IProjectService _projectService = projectService;

    [Route("projects")]
    public async Task<IActionResult> AlphaView()
    {
        var viewModel = new ProjectsViewModel()
        {
            Projects = await GetAllProjects(),
            AddProjectFormData = new AddProjectViewModel()
            {
                Clients = SetClients(),
            },
            EditProjectFormData = new EditProjectViewModel()
            {
                Clients = SetClients(),
                Statuses = SetStatuses(),
            }
        };
        return View(viewModel);
    }

    private async Task<IEnumerable<ProjectViewModel>> GetAllProjects()
    {
        
        var result = await _projectService.GetProjectsAsync();
        if (!result.Succeeded && result.Result == null)
        {
            return Enumerable.Empty<ProjectViewModel>();
        }
        if (result.Result != null)
        {   
            var projects = result.Result!.Select(p => new ProjectViewModel
            {
                ProjectName = p.ProjectName,
                Description = p.Description!,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Budget = p.Budget,
                ClientId = p.Client.Id,
                ClientName = p.Client.ClientName!,
                Image = p.Image!,
                StatusId = p.Status.Id,
            });
            return projects;
        }

        return Enumerable.Empty<ProjectViewModel>();
    }

    private IEnumerable<SelectListItem> SetClients()
    {
        var clients = new List<SelectListItem>();
        clients.Add(new SelectListItem
        {
            Value = "1",
            Text = "Github corp."
        });
        clients.Add(new SelectListItem
        {
            Value = "2",
            Text = "Gitlab inc."
        });
        return clients;
    }

    private IEnumerable<SelectListItem> SetStatuses()
    {
        var statuses = new List<SelectListItem>();
        statuses.Add(new SelectListItem
        {
            Value = Guid.NewGuid().ToString(),
            Text = "STARTED",
            Selected = true,
        });
        statuses.Add(new SelectListItem
        {
            Value = Guid.NewGuid().ToString(),
            Text = "COMPLETED"
        });
        return statuses;
    }
}