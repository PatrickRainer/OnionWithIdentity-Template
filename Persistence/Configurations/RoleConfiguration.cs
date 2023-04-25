using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Name = Values.OrgUserRole,
                NormalizedName = Values.OrgUserRole.ToUpper()
            },
            new IdentityRole
            {
                Name = Values.OrgAdminRole,
                NormalizedName = Values.OrgAdminRole.ToUpper()
            },
            new IdentityRole
            {
                Name = Values.SuperAdminRole,
                NormalizedName = Values.SuperAdminRole.ToUpper()
            });
    }
}