using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Web.DbInitializing;

public class DbInitializer
{
    public static void SeedUsers(UserManager<User> userManager)
    {
        var superAdminEmail = Values.SuperAdmin.Email;
        if (userManager.FindByEmailAsync(superAdminEmail).Result == null)
        {
            var user = new User
            {
                UserName = superAdminEmail,
                Email = superAdminEmail,
                OrganisationId = Values.MainOrganisationGuid
            };

            var result = userManager.CreateAsync(user, Values.SuperAdmin.Password).Result;

            if (result.Succeeded) userManager.AddToRoleAsync(user, Values.SuperAdminRole).Wait();
        }
    }
}