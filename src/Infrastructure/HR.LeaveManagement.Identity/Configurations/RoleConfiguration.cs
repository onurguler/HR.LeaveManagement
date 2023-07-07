using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "38a52ddd-c071-4b48-a211-9341d3adaa6a", 
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            },
            new IdentityRole
            {
                Id = "1b6e0df6-85bb-4325-95a5-61e9bee2dd90",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            }
        );
    }
}