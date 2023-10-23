using Bus_backUpData;
using Bus_backUpData.Models;
using Microsoft.Extensions.Options;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
Setting.ConnectionStrings = builder.Configuration.GetConnectionString("Connection");

SettingEmail.Email = builder.Configuration.GetValue<string>("SettingEmail:Email") ?? string.Empty;
SettingEmail.PassEmail = builder.Configuration.GetValue<string>("SettingEmail:PassEmail") ?? string.Empty;
SettingEmail.SubjectEmailNoti = builder.Configuration.GetValue<string>("SettingEmail:SubjectEmailNoti") ?? string.Empty;

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
