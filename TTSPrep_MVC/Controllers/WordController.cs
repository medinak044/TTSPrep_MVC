﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTSPrep_MVC.Helpers;
using TTSPrep_MVC.Models.ViewModels;
using TTSPrep_MVC.Models;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Controllers;

[Authorize(Roles = UserRoles.User)]
public class WordController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public WordController(
        IUnitOfWork unitOfWork
        )
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string projectId)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(projectId);

        return View(project);
    }

    [HttpGet]
    public async Task<IActionResult> ReturnToChapters(string projectId)
    {
        // Return user to chapters dashboard
        return RedirectToAction(nameof(ProjectController.Index), nameof(ProjectController).GetControllerName(),
            new { projectId = projectId });
    }

    [HttpGet]
    public async Task<IActionResult> ReturnToWords(string projectId)
    {
        // Return user to words list dashboard
        return RedirectToAction(nameof(WordController.Index), nameof(WordController).GetControllerName(),
            new { projectId = projectId });
    }

    [HttpGet]
    public async Task<IActionResult> Create(string projectId)
    {     
        var word = new Word()
        {
            ProjectId = projectId,
        };

        return View(word);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Word wordForm)
    {
        var word = new Word()
        {
            Id = Guid.NewGuid().ToString(),
            OriginalSpelling = wordForm.OriginalSpelling,
            ModifiedSpelling = wordForm.ModifiedSpelling ?? wordForm.OriginalSpelling,
            ProjectId = wordForm.ProjectId
        };

        await _unitOfWork.Words.AddAsync(word);
        if (!await _unitOfWork.SaveAsync())
        {
            TempData["error"] = "Something went wrong while saving";
            return View();
        }

        TempData["success"] = "Word succesfully created";
        return RedirectToAction(nameof(WordController.Index), nameof(WordController).GetControllerName(),
            new { projectId = wordForm.ProjectId });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string wordId)
    {
        var word = await _unitOfWork.Words.GetByIdAsync(wordId);
        if (word == null)
        {
            TempData["error"] = "Unable to get current user Id";
            return View("Error");
        }

        return View(word);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Word wordForm)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to edit");
            TempData["error"] = "ModelState is invalid";
            return View();
        }

        // Check if exists in db
        var word = await _unitOfWork.Words.GetByIdAsync(wordForm.Id);
        if (word == null)
        {
            TempData["error"] = "Id does not exist in database";
            return View();
        }

        // Overwrite values
        word.OriginalSpelling = wordForm.OriginalSpelling;
        word.ModifiedSpelling = wordForm.ModifiedSpelling;

        // Save changes
        await _unitOfWork.Words.UpdateAsync(word);
        if (!await _unitOfWork.SaveAsync())
        {
            TempData["error"] = "Something went wrong while saving";
            return View();
        }

        TempData["success"] = "Word updated";
        return RedirectToAction(nameof(WordController.Index), nameof(WordController).GetControllerName(),
            new { projectId = wordForm.ProjectId });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string wordId)
    {
        var word = await _unitOfWork.Words.GetByIdAsync(wordId);
        if (word == null)
        {
            TempData["error"] = "Unable to get current user Id";
            return View("Error");
        }

        return View(word);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Word wordForm) // Must submit an object to params of POST method
    {
        var word = await _unitOfWork.Words.GetByIdAsync(wordForm.Id);
        if (word == null)
        {
            TempData["error"] = "Unable to get current user Id";
            return View("Error");
        }

        await _unitOfWork.Words.RemoveAsync(word);
        if (!await _unitOfWork.SaveAsync())
        {
            TempData["error"] = "Something went wrong while saving";
            return View();
        }

        TempData["success"] = "Word deleted";
        return RedirectToAction(nameof(WordController.Index), nameof(WordController).GetControllerName(),
            new { projectId = wordForm.ProjectId });
    }
}
