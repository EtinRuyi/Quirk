using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quirk.Models.Domain;

namespace Quirk.Data
{
    public class QuirkDbContext : IdentityDbContext<IdentityUser>
    {
        public QuirkDbContext(DbContextOptions<QuirkDbContext> options) : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var SuperAdminRoleId = "df290ba0-1733-373c-8f1c-d687e5764168";
            var AdminRoleId = "ef290ba0-1733-4639-8f1c-d687e5764158";
            var UserRoleId = "cf290ba0-1733-4639-8f2c-d687e5764178";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = SuperAdminRoleId,
                    ConcurrencyStamp = SuperAdminRoleId,
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = AdminRoleId,
                    ConcurrencyStamp = AdminRoleId,
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = UserRoleId,
                    ConcurrencyStamp = UserRoleId,
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var SuperAdminId = "df290ba0-1722-373c-8f1c-d687e5764168";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@quirk.com",
                NormalizedUserName = "superadmin@quirk.com",
                Email = "superadmin@quirk.com",
                NormalizedEmail = "superadmin@quirk.com".ToUpper(),
                Id = SuperAdminId,
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "SuperAdmin123@");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = SuperAdminRoleId,
                    UserId = SuperAdminId,
                },
                new IdentityUserRole<string>
                {
                    RoleId = AdminRoleId,
                    UserId = SuperAdminId,
                },
                new IdentityUserRole<string>
                {
                    RoleId = UserRoleId,
                    UserId = SuperAdminId,
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
