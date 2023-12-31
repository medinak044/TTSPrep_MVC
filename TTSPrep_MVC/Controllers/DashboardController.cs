using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTSPrep_MVC.Helpers;
using TTSPrep_MVC.Models;
using TTSPrep_MVC.Models.ViewModels;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Controllers;

[Authorize(Roles = UserRoles.User)]
public class DashboardController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public DashboardController(
        IUnitOfWork unitOfWork
        )
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // Fill dashboard table with user's projects
        var currentUserId = _unitOfWork.GetCurrentUserId();
        if (string.IsNullOrEmpty(currentUserId))
        {
            TempData["error"] = "Unable to get current user Id";
            return View();
        }

        var projects = _unitOfWork.Projects.GetSome(p => p.OwnerId == currentUserId).ToList();
        // Organize projects by name
        //var tempProjectList = new List<Project>();
        //foreach (var p in projects)
        //{
        //    foreach (var p2 in tempProjectList)
        //    {

        //    }
        //    tempProjectList.Add(p);
        //}

        var dashboardVM = new DashboardVM()
        {
            Projects = projects
        };


        return View(dashboardVM);
    }

    //[HttpGet]
    //public async Task<IActionResult> Index()
    //{
    //    // Fill dashboard table with user's projects
    //    return View();
    //}
}
