using Bus_backUpData;
using Bus_IdentityUser;
using ModelProject.Models;
using ModelProject.EmailIdentity;
using Microsoft.Extensions.Options;
using Quartz;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AdminLayout_Vuexy;
using DalBackup.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Bus_IdentityUser.Services;
using Bus_backUpData.Services;
using Ninject.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();


Setting.ConnectionStrings = builder.Configuration.GetConnectionString("Connection");
Setting.ConnectionDefaut = builder.Configuration.GetConnectionString("ConnectionDefaut");
var isLog = false;
bool.TryParse(builder.Configuration.GetValue<string>("Setting:IsLog"), out isLog);
Setting.IsLog = isLog;

var connectionString = builder.Configuration.GetConnectionString("AdminLayout_VuexyContextConnection") ?? throw new InvalidOperationException("Connection string 'WebApplication6ContextConnection' not found.");

Setting.ConnectionSQLite = connectionString;

SettingEmail.Email = builder.Configuration.GetValue<string>("MailSettings:Mail") ?? string.Empty;
SettingEmail.PassEmail = builder.Configuration.GetValue<string>("MailSettings:Password") ?? string.Empty;
SettingEmail.SubjectEmailNoti = "System Backup Notification";
MailSettingCreate.Email = builder.Configuration.GetValue<string>("MailSettingCreate:Email") ?? string.Empty;
MailSettingCreate.UserName = builder.Configuration.GetValue<string>("MailSettingCreate:UserName") ?? string.Empty;
MailSettingCreate.PassWordDefault = builder.Configuration.GetValue<string>("MailSettingCreate:PassWordDefault") ?? string.Empty;
MailSettingCreate.DefaultLockoutTimeSpan = builder.Configuration.GetValue<int?>("MailSettingCreate:DefaultLockoutTimeSpan") ?? 5;
MailSettingCreate.MaxFailedAccessAttempts = builder.Configuration.GetValue<int?>("MailSettingCreate:MaxFailedAccessAttempts") ?? 5;
MailSettingCreate.AllowedForNewUsers = builder.Configuration.GetValue<bool?>("MailSettingCreate:AllowedForNewUsers") ?? false;
MailSettingCreate.RequireConfirmedEmail = builder.Configuration.GetValue<bool?>("MailSettingCreate:RequireConfirmedEmail") ?? false;
MailSettingCreate.RequireConfirmedPhoneNumber = builder.Configuration.GetValue<bool?>("MailSettingCreate:RequireConfirmedPhoneNumber") ?? false;
MailSettingCreate.RequireConfirmedAccount = builder.Configuration.GetValue<bool?>("MailSettingCreate:RequireConfirmedAccount") ?? false;
MailSettingCreate.ExpireTimeSpan = builder.Configuration.GetValue<int?>("MailSettingCreate:ExpireTimeSpan") ?? 30;




builder.Services.serviceDescriptorsAsync(builder.Configuration, connectionString);
builder.Services.serviceDescriptorsAsync(builder.Configuration);
builder.Services.AddNinject();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}


app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MigrateDatabase();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ConfigSetting}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapControllers();
app.Run();
