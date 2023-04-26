using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Web.DbInitializing;

public class DbInitializer
{
    public static void SeedUsers(UserManager<User> userManager)
    {
        var superAdminEmail = Values.SuperAdmin.Email;

        // Check if super admin exists
        var superAdmin = userManager.FindByEmailAsync(superAdminEmail).Result;
        if (superAdmin == null)
        {
            // If not exists, create super admin
            var user = new User
            {
                UserName = superAdminEmail,
                Email = superAdminEmail,
                OrganisationId = Values.MainOrganisationGuid
            };

            var result = userManager.CreateAsync(user, Values.SuperAdmin.Password).Result;

            if (result.Succeeded) userManager.AddToRoleAsync(user, Values.SuperAdminRole).Wait();
        }
        else
        {
            // If exists, check if super admin role is assigned
            if (!userManager.IsInRoleAsync(superAdmin, Values.SuperAdminRole).Result)
                userManager.AddToRoleAsync(superAdmin, Values.SuperAdminRole).Wait();
        }
    }
}