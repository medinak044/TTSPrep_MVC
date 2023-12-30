using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TTSPrep_MVC.Helpers;
using TTSPrep_MVC.Models;
using TTSPrep_MVC.Models.ViewModels;

namespace TTSPrep_MVC.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        var model = new LoginVM();
        return View(model);
    }

    #region Demo Login
    [HttpGet("DemoLogin/{email}")]
    public async Task<IActionResult> DemoLogin(string email)
    {
        string password = "Password!23";

        // Check if user exists
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser == null)
        {
            TempData["error"] = "Email doesn't exist";
            return View(new LoginVM());
        }

        // Verify password
        var passwordIsCorrect = await _userManager.CheckPasswordAsync(existingUser, password);
        if (!passwordIsCorrect)
        {
            TempData["error"] = "Invalid credentials";
            return View(new LoginVM());
        }

        // Log user in
        var signInResult = await _signInManager.PasswordSignInAsync(existingUser, password, false, false);
        if (!signInResult.Succeeded)
        {
            TempData["error"] = "Invalid credentials";
            return View(new LoginVM());
        }

        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).ControllerName());
    }
    #endregion

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (!ModelState.IsValid)
            return View(loginVM);

        // Check if user exists
        var existingUser = await _userManager.FindByEmailAsync(loginVM.Email);
        if (existingUser == null)
        {
            TempData["error"] = "Email doesn't exist";
            return View(loginVM);
        }

        // Verify password
        var passwordIsCorrect = await _userManager.CheckPasswordAsync(existingUser, loginVM.Password);
        if (!passwordIsCorrect)
        {
            TempData["error"] = "Invalid credentials";
            return View(loginVM);
        }

        // Log user in
        var signInResult = await _signInManager.PasswordSignInAsync(existingUser, loginVM.Password, false, false);
        if (!signInResult.Succeeded)
        {
            TempData["error"] = "Invalid credentials";
            return View(loginVM);
        }

        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).ControllerName());
    }

    [HttpGet]
    public async Task<IActionResult> SignUp()
    {
        var model = new SignUpVM();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpVM signUpVM)
    {
        if (!ModelState.IsValid)
            return View(signUpVM);

        // Check if email already exists
        var existingUser = await _userManager.FindByEmailAsync(signUpVM.Email);
        if (existingUser != null)
        {
            TempData["error"] = "This email is already in use";
            return View(signUpVM);
        }

        // Map values
        //var newUser = _mapper.Map<AppUser>(signUpVM);
        var newUser = new AppUser() 
        { 
            UserName = signUpVM.Email, // Identity requires UserName
            Email = signUpVM.Email,
            DateCreated = DateTime.Now,
        };

        var newUserResponse = await _userManager.CreateAsync(newUser, signUpVM.Password);
        if (!newUserResponse.Succeeded)
        {
            TempData["error"] = "Server error";
            return View(signUpVM);
        }

        // Assign default role to new user (Make sure roles exist in database first)
        await _userManager.AddToRoleAsync(newUser, "AppUser");

        // Log user in as a convenience
        var user = await _userManager.FindByEmailAsync(signUpVM.Email); // Track new user from db
        var isSignedIn = await _signInManager.PasswordSignInAsync(user, signUpVM.Password, false, false);
        if (!isSignedIn.Succeeded)
        {
            TempData["error"] = "Something went wrong while logging in. Please try again";
            return RedirectToAction("Login");
        }
       
        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).ControllerName());
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).ControllerName());
    }

    [HttpGet("Default")]
    public async Task<ActionResult> Default()
    {
        #region Account Roles (Identity framework)
        var accountRoleNames = new List<string>();

        if (await _roleManager.FindByNameAsync("AppUser") == null)
        {
            accountRoleNames.Add("AppUser");
        }
        if (await _roleManager.FindByNameAsync("Admin") == null)
        {
            accountRoleNames.Add("Admin");
        }

        foreach (var accountRoleName in accountRoleNames)
        {
            await _roleManager.CreateAsync(new IdentityRole(accountRoleName));
        }
        #endregion

        #region AppUsers (Identity framework)
        var demoIdentityPassword = "Password!23";

        if (await _userManager.FindByEmailAsync("admin@example.com") == null)
        {
            var adminUser = new AppUser()
            {
                Email = "admin@example.com", // Email serves as username in TTSPrep app
            };
            await _userManager.CreateAsync(adminUser, demoIdentityPassword);
            // After user is created, add role
            var foundUser = await _userManager.FindByEmailAsync(adminUser.Email);
            await _userManager.AddToRoleAsync(foundUser, "AppUser");
            await _userManager.AddToRoleAsync(foundUser, "Admin");
        }

        if (await _userManager.FindByEmailAsync("appuser@example.com") == null)
        {
            var appUser = new AppUser()
            {
                Email = "appuser@example.com",
            };
            await _userManager.CreateAsync(appUser, demoIdentityPassword);
            // After user is created, add role
            var foundUser = await _userManager.FindByEmailAsync(appUser.Email);
            await _userManager.AddToRoleAsync(foundUser, "AppUser");
        }
        #endregion

        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).ControllerName());
    }
}
