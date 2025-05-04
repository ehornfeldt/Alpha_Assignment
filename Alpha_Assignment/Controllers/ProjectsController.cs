using Business.Services;
using Data.Entities;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Presentation.Models;

namespace Presentation.Controllers;

[Authorize]
public class ProjectsController(IProjectService projectService) : Controller
{
    private readonly IProjectService _projectService = projectService;

    [HttpPost]
    public async Task<IActionResult> AddProject(AddProjectViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["ShowAddModal"] = true;
            return RedirectToAction("AlphaView", "Alpha");
            //return PartialView("Partials/_AddProjectModalPartial", model);
        }


        var addProjectFormData = model.MapTo<AddProjectFormData>();
        addProjectFormData.StatusId = 1;
        addProjectFormData.Image = "/assets/card-icon.svg";
        var result = await _projectService.CreateProjectsAsync(addProjectFormData);
        if (result.Succeeded)
        {
            Console.WriteLine("Project added successfully");
            return RedirectToAction("AlphaView", "Alpha");
        }

        return RedirectToAction("AlphaView", "Alpha");

    }

    [HttpPost]
    public async Task<IActionResult> EditProject(EditProjectViewModel model, string id)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("AlphaView", "Alpha");
        }

        var editProjectFormData = model.MapTo<EditProjectFormData>();
        editProjectFormData.Image = "/assets/card-icon.svg";

        var result = await _projectService.EditProjectAsync(model.Id, editProjectFormData);
        if (result.Succeeded)
        {
            Console.WriteLine("Project updated successfully");
            return RedirectToAction("AlphaView", "Alpha");
        }

        return RedirectToAction("AlphaView", "Alpha");
    }
}
