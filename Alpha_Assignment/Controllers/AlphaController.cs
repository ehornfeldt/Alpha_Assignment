using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;
public class AlphaController : Controller
{
    [Route("projects")]
    public IActionResult AlphaView()
    {
        var viewModel = new ProjectsViewModel()
        {
            Projects = SetProjects()
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
            ClientName = "Github corp."
        });

        projects.Add(new ProjectViewModel
        {
            Id = Guid.NewGuid().ToString(),
            Image = "/assets/card-icon.svg",
            ProjectName = "Project 2",
            Description = "Description for project 2",
            ClientName = "Gitlab inc."
        });

        return projects;
    }
}