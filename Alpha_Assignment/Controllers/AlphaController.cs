using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Models;

namespace Presentation.Controllers;
public class AlphaController : Controller
{
    [Route("projects")]
    public IActionResult AlphaView()
    {
        var viewModel = new ProjectsViewModel()
        {
            Projects = SetProjects(),
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

    private IEnumerable<ProjectViewModel> SetProjects()
    {
        var projects = new List<ProjectViewModel>();
        projects.Add(new ProjectViewModel
        {
            Id = Guid.NewGuid().ToString(),
            Image = "/assets/card-icon.svg",
            ProjectName = "Project 1",
            Description = "Description for project 1",
            ClientName = "Github corp.",
            ClientId = "1",
            Clients = SetClients(),
            StartDate = DateTime.Now,
            Budget = 1000,
        });

        projects.Add(new ProjectViewModel
        {
            Id = Guid.NewGuid().ToString(),
            Image = "/assets/card-icon.svg",
            ProjectName = "Project 2",
            Description = "Description for project 2",
            ClientName = "Gitlab inc.",
            ClientId = "2",
            Clients = SetClients(),
            StartDate = DateTime.Now,
            Budget = 2000,
        });

        return projects;
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