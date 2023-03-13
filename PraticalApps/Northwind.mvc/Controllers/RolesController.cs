using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using static System.Console;

namespace Northwind.mvc.Controllers;

public class RolesController : Controller
{
    private string AdminRole = "Administrators";
    private string UserEmail = "test@example.com";

    private readonly RoleManager<IdentityRole> roleManager;
    private readonly UserManager<IdentityUser> UserManager;

    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> UserManager)
    {
        this.roleManager = roleManager; 
        this.UserManager = UserManager; 
    }
    public async Task<IActionResult> Index()    
    {
       if (!(await roleManager.RoleExistsAsync(AdminRole)))
        {
            await roleManager.CreateAsync(new IdentityRole(AdminRole));
        }
       IdentityUser user = await UserManager.FindByEmailAsync(UserEmail);

        if (user == null)
        {
            user = new();
            user.UserName = UserEmail;
            user.Email = UserEmail;
            IdentityResult result = await UserManager.CreateAsync(user, "Pa$$w0rd");

            if (result.Succeeded)
            {
                WriteLine($"User {user.UserName} created successfully.");
            }
            else 
            {
                foreach(IdentityError error in result.Errors) 
                {
                    WriteLine(error.Description);
                }
            }
        }

        if (!user.EmailConfirmed) 
        {
            string token = await UserManager.GenerateEmailConfirmationTokenAsync(user);

            IdentityResult result = await UserManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                WriteLine($"User {user.UserName} email confirmed successfully.");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    WriteLine(error.Description);
                }
            }
        }

        if (!(await UserManager.IsInRoleAsync(user, AdminRole)))
        {
            IdentityResult result = await UserManager.AddToRoleAsync(user, AdminRole);

            if (result.Succeeded)
            {
                WriteLine($"User {user.UserName} added to {AdminRole}");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    WriteLine(error.Description);
                }
            }
        }
        return Redirect("/");
    }
}
