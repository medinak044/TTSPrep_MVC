using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTSPrep_MVC.Helpers;
using TTSPrep_MVC.Models;
using TTSPrep_MVC.Models.ViewModels;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Controllers;

[Authorize(Roles = UserRoles.User)]
public class ProjectController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectController(
        IUnitOfWork unitOfWork
        )
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string projectId)
    {
        // Display the single project page view, complete with text features
        var currentUserId = _unitOfWork.GetCurrentUserId();
        if (string.IsNullOrEmpty(currentUserId))
        {
            TempData["error"] = "Unable to get current user Id";
            return View();
        }

        // Get the project data
        var project = await _unitOfWork.Projects.GetByIdAsync(projectId);

        // Wait for the project object data to load before making more db calls
        if (project != null)
        {
            // Get the chapter data (rework this to use Include() for eager loading so that all data is gathered in one call)
            project.Chapters = _unitOfWork.Chapters.GetSome(c => c.ProjectId == project.Id).ToList();
            // Word data associated with project
            project.Words = _unitOfWork.Words.GetSome(w => w.ProjectId == project.Id).ToList();
        }

        return View(project);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var currentUserId = _unitOfWork.GetCurrentUserId();
        if (string.IsNullOrEmpty(currentUserId))
        {
            TempData["error"] = "Unable to get current user Id";
            return View();
        }
        var projectVM = new ProjectCreateVM()
        { 
            OwnerId = currentUserId
        };

        return View(projectVM);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectCreateVM projectCreateVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to create");
            TempData["error"] = "ModelState is invalid";
            return View();
        }


        var newId = Guid.NewGuid().ToString();
        var time = DateTime.Now;
        var currentUserId = _unitOfWork.GetCurrentUserId();
        if (string.IsNullOrEmpty(currentUserId))
        {
            TempData["error"] = "Unable to get current user Id";
            return View();
        }

        var project = new Project()
        {
            Id = newId,
            Title = projectCreateVM.Title ?? $"project-{newId}",
            Description = projectCreateVM.Description,
            CreatedDate = time,
            LastModifiedDate = time, // Same as time when created
            OwnerId = currentUserId
        };

        await _unitOfWork.Projects.AddAsync(project);
        if (!await _unitOfWork.SaveAsync())
        {
            TempData["error"] = "Something went wrong while saving";
            return View();
        }

        TempData["success"] = "Project succesfully created";
        return RedirectToAction(nameof(DashboardController.Index), nameof(DashboardController).GetControllerName());
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string projectId)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(projectId);
        if (project == null) 
        { 
            TempData["error"] = "Unable to get current user Id"; 
            return View("Error");
        }
        var projectEditVM = new ProjectEditVM
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
        };

        return View(projectEditVM);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProjectEditVM projectEditVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to edit");
            TempData["error"] = "ModelState is invalid";
            return View();
        }

        // Check if exists in db
        var project = await _unitOfWork.Projects.GetByIdAsync(projectEditVM.Id);
        if (project == null)
        {
            TempData["error"] = "Id does not exist in database";
            return View();
        }

        // Overwrite values
        project.Title = projectEditVM.Title;
        project.Description = projectEditVM.Description;
        project.LastModifiedDate = DateTime.Now;

        // Save changes
        await _unitOfWork.Projects.UpdateAsync(project);
        if (!await _unitOfWork.SaveAsync())
        {
            TempData["error"] = "Something went wrong while saving";
            return View();
        }

        TempData["success"] = "Project updated";
        return RedirectToAction(nameof(DashboardController.Index), nameof(DashboardController).GetControllerName());
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string projectId)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(projectId);
        if (project == null)
        {
            TempData["error"] = "Unable to get current user Id";
            return View("Error");
        }

        return View(project);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePOST(Project projectTemp)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(projectTemp.Id);
        if (project == null)
        {
            TempData["error"] = "Unable to get current user Id";
            return View("Error");
        }

        await _unitOfWork.Projects.RemoveAsync(project);
        if (!await _unitOfWork.SaveAsync())
        {
            TempData["error"] = "Something went wrong while saving";
            return View();
        }

        TempData["success"] = "Project deleted";
        return RedirectToAction(nameof(DashboardController.Index), nameof(DashboardController).GetControllerName());
    }
}
