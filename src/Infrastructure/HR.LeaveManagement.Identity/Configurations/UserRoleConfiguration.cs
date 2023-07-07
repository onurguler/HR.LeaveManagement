using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>()
            {
                RoleId = "1b6e0df6-85bb-4325-95a5-61e9bee2dd90", UserId = "91582c2a-9fa3-458d-a668-ebda3a76c02c",
            },
            new IdentityUserRole<string>()
            {
                RoleId = "38a52ddd-c071-4b48-a211-9341d3adaa6a", UserId = "d37e25a5-6f8d-4fb4-a91c-e881b6b47120",
            }
        );
    }
}