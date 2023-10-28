using Dal_IdentityUser.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelProject.EmailIdentity;
using ModelProject.Models;

namespace Dal_IdentityUser.Data;

public class AdminLayout_VuexyContext : IdentityDbContext<AdminLayout_VuexyUser>
{
    public AdminLayout_VuexyContext(DbContextOptions<AdminLayout_VuexyContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        //admin pass: abc123@ -> change later
        //builder.Entity<IdentityUserRole<Guid>>(eb =>
        //{
        //    eb.HasNoKey();
        //});
        var hasher = new PasswordHasher<IdentityUser>();
        var user = Activator.CreateInstance<AdminLayout_VuexyUser>();
        user.UserName = "Admin";
        user.Email  = "thinh48691953@gmail.com";
        user.NormalizedUserName = "ADMIN";
        user.NormalizedEmail = "THINH48691953@GMAIL.COM";
        user.LockoutEnabled = true;
        user.EmailConfirmed = false;
        user.PasswordHash = hasher.HashPassword(user, "Vnr@123");


        builder.Entity<AdminLayout_VuexyUser>()
        .HasData(
            user
        );
       
    }

}
