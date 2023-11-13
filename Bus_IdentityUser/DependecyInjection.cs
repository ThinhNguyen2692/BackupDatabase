using Bus_IdentityUser.Interface;
using Bus_IdentityUser.Services;
using DalBackup.Data;
using DalBackup.Interface;
using DalBackup.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModelProject.EmailIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_IdentityUser
{
    public static class DependecyInjection
    {
        public static async Task<IServiceCollection> serviceDescriptorsAsync(this IServiceCollection services, IConfiguration configuration, string connectionString)
        {
            services.AddDbContext<AdminLayout_VuexyContext>(options => options.UseSqlite(connectionString));
            services.AddDefaultIdentity<AdminLayout_VuexyUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AdminLayout_VuexyContext>();
            services.Configure<IdentityOptions>(options => {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;
                options.SignIn.RequireConfirmedEmail = false;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });
            services.ConfigureApplicationCookie(options => {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.SlidingExpiration = true;
            });
            services.AddOptions();                                        // Kích hoạt Options
            var mailsettings = configuration.GetSection("MailSettings");
            MailSettingCreate.Email = configuration.GetValue<string>("MailSettings:Mail") ?? string.Empty;
            // đọc config
            services.Configure<MailSettings>(mailsettings);               // đăng ký để Inject
            services.AddTransient<IEmailSender, SendMailService>();
            services.AddScoped<IBusUserManager, BusUserManager>();
            services.AddScoped<IDalUser, DalUser>();
            return services;
        }
    }
}
