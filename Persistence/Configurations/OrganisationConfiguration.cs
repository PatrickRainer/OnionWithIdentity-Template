using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class OrganisationConfiguration : IEntityTypeConfiguration<Organisation>
{
    public void Configure(EntityTypeBuilder<Organisation> builder)
    {
        builder.ToTable(nameof(Organisation));

        builder.HasKey(organisation => organisation.Id);

        builder.Property(user => user.Id).ValueGeneratedOnAdd();

        builder.Property(organisation => organisation.Name).HasMaxLength(60);

        builder.Property(organisation => organisation.Address).HasMaxLength(100);

        builder.HasMany(organisation => organisation.users)
            .WithOne()
            .HasForeignKey(user => user.OrganisationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Organisation
            {
                Id = Values.MainOrganisationGuid,
                Name = "Main Organisation",
                Address = "Organisation Address"
            });
    }
}