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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();


Setting.ConnectionStrings = builder.Configuration.GetConnectionString("Connection");
Setting.ConnectionDefaut = builder.Configuration.GetConnectionString("ConnectionDefaut");
var connectionString = builder.Configuration.GetConnectionString("AdminLayout_VuexyContextConnection") ?? throw new InvalidOperationException("Connection string 'WebApplication6ContextConnection' not found.");



SettingEmail.Email = builder.Configuration.GetValue<string>("SettingEmailNoti:Email") ?? string.Empty;
SettingEmail.PassEmail = builder.Configuration.GetValue<string>("SettingEmailNoti:PassEmail") ?? string.Empty;
SettingEmail.SubjectEmailNoti = builder.Configuration.GetValue<string>("SettingEmailNoti:SubjectEmailNoti") ?? string.Empty;
MailSettingCreate.Email = builder.Configuration.GetValue<string>("MailSettingCreate:Email") ?? string.Empty;
MailSettingCreate.UserName = builder.Configuration.GetValue<string>("MailSettingCreate:UserName") ?? string.Empty;
MailSettingCreate.PassWordDefault = builder.Configuration.GetValue<string>("MailSettingCreate:PassWordDefault") ?? string.Empty;

builder.Services.serviceDescriptorsAsync(builder.Configuration, connectionString);
builder.Services.serviceDescriptorsAsync(builder.Configuration);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ConfigSetting}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapControllers();
app.Run();
