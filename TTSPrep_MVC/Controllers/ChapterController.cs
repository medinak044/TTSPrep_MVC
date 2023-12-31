using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTSPrep_MVC.Helpers;

namespace TTSPrep_MVC.Controllers;

[Authorize(Roles = UserRoles.User)]
public class ChapterController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    }


}
