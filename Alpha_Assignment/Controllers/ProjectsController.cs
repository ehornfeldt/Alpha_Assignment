using Business.Services;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

public class ProjectsController(IProjectService projectService) : Controller
{
    private readonly IProjectService _projectService = projectService;

    //public async Task<IActionResult> Index()
    //[Route("projects")]
    public IActionResult Index()
    {
        var viewModel = new ProjectsViewModel();
        //{
        //    Projects = await _projectService.GetProjectsAsync(),
        //};
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddProject(AddProjectViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Hämta Clients igen om modellen är ogiltig (SelectList återställs annars)
            //model.Clients = GetClients();
            //
            ViewData["ShowAddModal"] = true;
            return RedirectToAction("AlphaView", "Alpha");
            //return PartialView("Partials/_AddProjectModalPartial", model);
        }


        var addProjectFormData = model.MapTo<AddProjectFormData>();
        addProjectFormData.StatusId = 1; 
        var result = await _projectService.CreateProjectsAsync(addProjectFormData);
        if (result.Succeeded)
        {
            Console.WriteLine("Project added successfully");
            return RedirectToAction("AlphaView", "Alpha");
        }

        return RedirectToAction("AlphaView", "Alpha");

        //return Json(new { });
    }

    [HttpPut]
    public IActionResult EditProject(EditProjectViewModel model)
    {
        return RedirectToAction("AlphaView", "Alpha");
    }

    //[HttpDelete]
    //public IActionResult Delete(string id)
    //{
    //    return Json(new { });
    //}
}
