using Microsoft.AspNetCore.Identity;
using TTSPrep_MVC.Helpers;
using TTSPrep_MVC.Models;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Data;

public class DbInitializer: IDbInitializer
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbInitializer(
        IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager
        )
    {
        _unitOfWork = unitOfWork;
        _roleManager = roleManager;
        _userManager = userManager;
    }


    public async Task Initialize()
    {
        //// Validations
        //var result = new RequestResult()
        //{
        //    Success = true,
        //    Messages = new List<string>()
        //};

        #region Account roles (Identity framework)
        if (await _roleManager.FindByNameAsync(UserRoles.User.ToLower()) == null)
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        }
        if (await _roleManager.FindByNameAsync(UserRoles.Admin.ToLower()) == null)
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        }
        #endregion

        #region App users (Identity framework)
        var demoIdentityPassword = "Password!23";
        if (await _userManager.FindByEmailAsync("admin@example.com") == null)
        {
            var adminUser = new AppUser()
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                DateCreated = DateTime.Now,
            };
            await _userManager.CreateAsync(adminUser, demoIdentityPassword);
            // After user is created, add role
            var foundUser = await _userManager.FindByEmailAsync(adminUser.Email);
            await _userManager.AddToRoleAsync(foundUser, UserRoles.User);
            await _userManager.AddToRoleAsync(foundUser, UserRoles.Admin);
            //result.Messages.Add("Admin user added");
        }
        if (await _userManager.FindByEmailAsync("appuser@example.com") == null)
        {
            var appUser = new AppUser()
            {
                UserName = "appuser@example.com",
                Email = "appuser@example.com",
                DateCreated = DateTime.Now,
            };
            await _userManager.CreateAsync(appUser, demoIdentityPassword);
            // After user is created, add role
            var foundUser = await _userManager.FindByEmailAsync(appUser.Email);
            await _userManager.AddToRoleAsync(foundUser, UserRoles.User);
            //result.Messages.Add("App user added");
        }

        #endregion

    }
}
