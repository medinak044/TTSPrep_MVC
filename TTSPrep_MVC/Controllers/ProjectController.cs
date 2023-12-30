using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Controllers;

//[Authorize(Roles = "AppUser")]
public class ProjectController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        return View();
    }
}
