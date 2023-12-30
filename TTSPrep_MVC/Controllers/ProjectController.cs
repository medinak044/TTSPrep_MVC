using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTSPrep_MVC.Helpers;
using TTSPrep_MVC.Models.ViewModels;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Controllers;

[Authorize(Roles = UserRoles.User)]
public class ProjectController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProjectController(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
        )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        //var currentUserID = _httpContextAccessor.HttpContext?.User.GetUserId();
        //var projectVM = new ProjectVM { OwnerId = currentUserID };
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectVM projectVM)
    {
        // Use the submitted project form from 
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Create project failed");
        }
        // Id = Guid.NewGuid().ToString();

        TempData["success"] = "Project succesfully created";
        return View();
        //return RedirectToAction(nameof(ProjectController.Index), nameof(ProjectController).GetControllerName());
    }
    // How do I get the current user so that the app can load the appropriate data
}
