using HR.LeaveManagement.Identity.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        var users = new List<ApplicationUser>()
        {
            new()
            {
                Id = "91582c2a-9fa3-458d-a668-ebda3a76c02c",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                FirstName = "System",
                LastName = "Admin",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                EmailConfirmed = true
            },
            new()
            {
                Id = "d37e25a5-6f8d-4fb4-a91c-e881b6b47120",
                Email = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                FirstName = "System",
                LastName = "User",
                UserName = "user@localhost.com",
                NormalizedUserName = "user@LOCALHOST.COM",
                EmailConfirmed = true
            }
        };

        users.ForEach(user =>
        {
            user.PasswordHash = hasher.HashPassword(user, "P@ssword1");
        });

        builder.HasData(users);
    }
}