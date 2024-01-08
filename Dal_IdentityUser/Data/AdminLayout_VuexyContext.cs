using DalBackup.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelProject.EmailIdentity;
using ModelProject.Models;
using ModelProject.Models.Enum;
namespace DalBackup.Data;

public class AdminLayout_VuexyContext : IdentityDbContext<AdminLayout_VuexyUser>
{
    public AdminLayout_VuexyContext(DbContextOptions<AdminLayout_VuexyContext> options)
        : base(options)
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(Setting.ConnectionSQLite);
        }
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
        user.UserName = MailSettingCreate.UserName;
        user.Email  = MailSettingCreate.Email;
        user.NormalizedUserName = MailSettingCreate.UserName.ToUpper();
        user.NormalizedEmail = MailSettingCreate.Email.ToUpper();
        user.LockoutEnabled = true;
        user.EmailConfirmed = false;
        user.PasswordHash = hasher.HashPassword(user, MailSettingCreate.PassWordDefault);

        builder.Entity<AdminLayout_VuexyUser>()
        .HasData(
            user
        );
        List<Weekly> weeklies = new List<Weekly>() { 
            new Weekly() {Id = Guid.Parse("C8B1413F-2F1D-41CF-96EB-AA7EAF9714E4"), Name = "Monday", Value = 2, CreatedDate = DateTimeOffset.Now, UpdatedDate = DateTimeOffset.Now },
            new Weekly() {Id = Guid.Parse("A1907086-A621-4A7F-9784-FE7A65A9B01A"), Name = "Tuesday", Value = 4, CreatedDate = DateTimeOffset.Now, UpdatedDate = DateTimeOffset.Now },
            new Weekly() {Id = Guid.Parse("A35339F6-AB5E-4197-825A-424A3888D7FB"), Name = "Wednesday", Value = 8, CreatedDate = DateTimeOffset.Now, UpdatedDate = DateTimeOffset.Now },
            new Weekly() {Id = Guid.Parse("3FE18C8D-CFDC-419A-A848-E07D8B383386"), Name = "Thursday", Value = 16, CreatedDate = DateTimeOffset.Now, UpdatedDate = DateTimeOffset.Now },
            new Weekly() {Id = Guid.Parse("8C6D144E-2FAD-4CC2-A1AD-FE7661110F31"), Name = "Friday", Value = 32, CreatedDate = DateTimeOffset.Now, UpdatedDate = DateTimeOffset.Now },
            new Weekly() {Id = Guid.Parse("44AE543B-1C51-4E79-81F0-E1D99E274162"), Name = "Saturday", Value = 64, CreatedDate = DateTimeOffset.Now, UpdatedDate = DateTimeOffset.Now },
            new Weekly() {Id = Guid.Parse("3C2E8836-3745-451B-826D-DA952513E0A6"), Name = "Sunday", Value = 1, CreatedDate = DateTimeOffset.Now, UpdatedDate = DateTimeOffset.Now }
        };
        builder.Entity<Weekly>()
       .HasData(weeklies);

    }
    public virtual DbSet<Weekly> Weeklies { get; set; }
    public virtual DbSet<BackUpSetting> BackUpSettings { get; set; }
    public virtual DbSet<EmailConfirmation> EmailConfirmations { get; set; }
    public virtual DbSet<FTPSetting> FTPSettings { get; set; }
    public virtual DbSet<HistoryFTP> HistoryFTPs { get; set; }
    public virtual DbSet<ScheduleBackup> ScheduleBackups { get; set; }
    public virtual DbSet<ScheduleBackupWeekly> ScheduleBackupWeeklies { get; set; }
    public virtual DbSet<ConfigurationBackUp> ConfigurationBackUps { get; set; }
}
