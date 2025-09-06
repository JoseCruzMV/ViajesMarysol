using Microsoft.AspNetCore.Identity;
using ViajesMarysol.Models.Users;

namespace ViajesMarysol.Data.MockData;

public class MockData
{
    public async static Task InitializeAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        // ---Default admin user---
        string adminRoleName = "Admin";

        if (await roleManager.FindByNameAsync(adminRoleName) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName: adminRoleName));
        }


        string adminEmail = "admin@admin.com";
        string adminPassword = "admin";

        if (await userManager.FindByNameAsync(adminEmail) == null)
        {
            ApplicationUser adminUser = new()
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };
            IdentityResult result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, adminRoleName);
            }
        }
    }
}
