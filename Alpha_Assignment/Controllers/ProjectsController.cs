using Business.Services;
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
    //[HttpPost]
    //public async Task<IActionResult> Add(AddProjectViewModel model)
    //{
    //    var addProjectFormData = model.MapTo<AddProjectFormData>();
    //    var result = await _projectService.CreateProjectsAsync(addProjectFormData);
    //    return Json(new { });
    //}

    //[HttpPut]
    //public IActionResult Update(UpdateProjectViewModel model)
    //{
    //    return Json(new { });
    //}

    //[HttpDelete]
    //public IActionResult Delete(string id)
    //{
    //    return Json(new { });
    //}
}
