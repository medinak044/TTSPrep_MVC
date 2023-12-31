﻿using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> Index()
    {
        // Display the single project page view, complete with text features
        var currentUserId = _unitOfWork.GetCurrentUserId();
        if (string.IsNullOrEmpty(currentUserId))
        {
            TempData["error"] = "Unable to get current user Id";
            return View();
        }

        // Get the project data


        return View();
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
            ModelState.AddModelError("", "Create project failed");
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


    [HttpGet]
    public async Task<IActionResult> Delete(string projectId)
    {
        // Display the project details
        var project = await _unitOfWork.Projects.GetByIdAsync(projectId);
        //var project = _unitOfWork.Projects.GetSome(p => p.Id == projectId).ToList();
        if (project == null) { TempData["error"] = "Unable to get current user Id"; }
        return View(project);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteProject(string projectId)
    {
        // Delete project
        TempData["success"] = "Project deleted";
        return RedirectToAction(nameof(DashboardController.Index), nameof(DashboardController).GetControllerName());
    }
}