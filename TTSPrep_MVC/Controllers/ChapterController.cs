using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTSPrep_MVC.Helpers;
using TTSPrep_MVC.Models;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Controllers;

[Authorize(Roles = UserRoles.User)]
public class ChapterController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ChapterController(
        IUnitOfWork unitOfWork
        )
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> Create(string projectId)
    {
        var currentUserId = _unitOfWork.GetCurrentUserId();
        if (string.IsNullOrEmpty(currentUserId))
        {
            TempData["error"] = "Unable to get current user Id";
            return View();
        }
        // Get a list of all chapters of the project
        var chapters = _unitOfWork.Chapters.GetSome(c => c.ProjectId == projectId).ToList();
        // The new chapter must have an order number increment the highest number by 1
        int orderNumber = 0; 
        foreach (var c in chapters)
        {
            if (c.OrderNumber > orderNumber)
                orderNumber = c.OrderNumber;
        }
        orderNumber += 1; // Order number must at least start at 1

        var chapter = new Chapter()
        {
            OrderNumber = orderNumber,
            ProjectId = projectId
        };

        return View(chapter); // View of chapter creation form
    }

    [HttpPost]
    public async Task<IActionResult> Create(Chapter chapterForm)
    {
        var chapter = new Chapter() 
        {
            Id = Guid.NewGuid().ToString(),
            Title = chapterForm.Title ?? $"Chapter {chapterForm.OrderNumber}",
            OrderNumber = chapterForm.OrderNumber,
            ProjectId = chapterForm.ProjectId
        };

        await _unitOfWork.Chapters.AddAsync(chapter);
        if (!await _unitOfWork.SaveAsync())
        {
            TempData["error"] = "Something went wrong while saving";
            return View();
        }


        // Redirect back to chapter dashboard
        TempData["success"] = "Chapter succesfully created";
        return RedirectToAction(nameof(ProjectController.Index), nameof(ProjectController).GetControllerName(), 
            new { projectId = chapterForm.ProjectId });
    }


    
    [HttpGet]
    public async Task<IActionResult> Edit(string chapterId)
    {
        var chapter = await _unitOfWork.Chapters.GetByIdAsync(chapterId);
        if (chapter == null)
        {
            TempData["error"] = "Unable to get chapter data";
            return View("Error");
        }
        else
        {
            // Assign chapter as current chapter of project
            // (Implement this in the Angular/.NET project)
        }

        return View(chapter);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Chapter chapterForm)
    {
        var chapter = await _unitOfWork.Chapters.GetByIdAsync(chapterForm.Id);
        if (chapter == null)
        {
            TempData["error"] = "Unable to get chapter data";
            return View("Error");
        }

        chapter.Title = chapterForm.Title;
        //chapter.OrderNumber = chapterForm.OrderNumber; // (Include logic that handles when order number changes, change the order number of other chapters)

        // Clearing original text: If text box is empty, make both original and modified text null
        if (string.IsNullOrEmpty(chapterForm.ModifiedText))
        {
            chapter.OriginalText = string.Empty;
            chapter.ModifiedText = string.Empty;
        }
        // Submitting original text: If original text is null, save text as original and modified text
        else if (string.IsNullOrEmpty(chapter.OriginalText))
        {
            
            chapter.OriginalText = chapterForm.ModifiedText;
            chapter.ModifiedText = chapterForm.ModifiedText;
        }
        // Saving modified text: If original text is not null, save text as modified text
        else if (!string.IsNullOrEmpty(chapter.OriginalText))
        {
            chapter.ModifiedText = chapterForm.ModifiedText;
        }

        await _unitOfWork.Chapters.UpdateAsync(chapter);
        if (!await _unitOfWork.SaveAsync())
        {
            TempData["error"] = "Something went wrong while saving";
            return View();
        }

        TempData["success"] = "Chapter updated";
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string chapterId)
    {
        var chapter = await _unitOfWork.Chapters.GetByIdAsync(chapterId);
        if (chapter == null)
        {
            TempData["error"] = "Unable to get chapter data";
            return View("Error");
        }

        return View(chapter);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Chapter chapterForm)
    {
        var chapter = await _unitOfWork.Chapters.GetByIdAsync(chapterForm.Id);
        if (chapter == null)
        {
            TempData["error"] = "Unable to get chapter data";
            return View("Error");
        }

        await _unitOfWork.Chapters.RemoveAsync(chapter);
        if (!await _unitOfWork.SaveAsync())
        {
            TempData["error"] = "Something went wrong while saving";
            return View();
        }

        TempData["success"] = "Chapter deleted";
        return RedirectToAction(nameof(ProjectController.Index), nameof(ProjectController).GetControllerName(),
            new { projectId = chapterForm.ProjectId });
    }
}
