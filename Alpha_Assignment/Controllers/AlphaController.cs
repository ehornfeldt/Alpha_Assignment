using Business.Services;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Models;

namespace Presentation.Controllers;

[Authorize]
public class AlphaController(IProjectService projectService, IClientService clientService, IStatusService statusService, UserManager<UserEntity> userManager) : Controller
{
    private readonly IProjectService _projectService = projectService;
    private readonly IClientService _clientService = clientService;
    private readonly IStatusService _statusService = statusService;
    private readonly UserManager<UserEntity> _userManager = userManager;

    [Route("projects")]
    public async Task<IActionResult> AlphaView()
    {

        var user = await _userManager.GetUserAsync(User);
        var fullName = $"{user!.FirstName} {user.LastName}";
        ViewBag.FullName = fullName;

        var viewModel = new ProjectsViewModel()
        {
            Projects = await GetAllProjects(),
            AddProjectFormData = new AddProjectViewModel()
            {
                Clients = await GetAllClients(),
            },
            EditProjectFormData = new EditProjectViewModel()
            {
                Clients = await GetAllClients(),
                Statuses = await GetAllStatuses(),
            }
        };
        return View(viewModel);
    }

    private async Task<IEnumerable<ProjectViewModel>> GetAllProjects()
    {

        var result = await _projectService.GetProjectsAsync();
        var clients = await GetAllClients();
        var statuses = await GetAllStatuses();
        if (!result.Succeeded && result.Result == null)
        {
            return Enumerable.Empty<ProjectViewModel>();
        }
        if (result.Result != null)
        {
            var projects = result.Result!.Select(p => new ProjectViewModel
            {
                Id = p.Id,
                ProjectName = p.ProjectName,
                Description = p.Description!,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Budget = p.Budget,
                ClientId = p.Client.Id,
                ClientName = p.Client.ClientName!,
                Image = p.Image!,
                StatusId = p.Status.Id,
                Clients = clients,
                Statuses = statuses,
            });
            return projects;
        }

        return Enumerable.Empty<ProjectViewModel>();
    }

    private async Task<IEnumerable<SelectListItem>> GetAllClients()
    {
        var clients = new List<SelectListItem>();
        var result = await _clientService.GetClientsAsync();
        if (!result.Succeeded && result.Result == null)
        {
            return new List<SelectListItem>();
        }
        if (result.Result != null)
        {

            foreach (var client in result.Result)
            {
                clients.Add(new SelectListItem
                {
                    Value = client.Id.ToString(),
                    Text = client.ClientName!,
                });
            }
            return clients;
        }

        return new List<SelectListItem>();
    }

    private async Task<IEnumerable<SelectListItem>> GetAllStatuses()
    {
        var statuses = new List<SelectListItem>();
        var result = await _statusService.GetStatusesAsync();
        if (!result.Succeeded && result.Result == null)
        {
            return new List<SelectListItem>();
        }
        if (result.Result != null)
        {

            foreach (var status in result.Result)
            {
                statuses.Add(new SelectListItem
                {
                    Value = status.Id.ToString(),
                    Text = status.StatusName!,
                });
            }
            return statuses;
        }

        return new List<SelectListItem>();
    }
}