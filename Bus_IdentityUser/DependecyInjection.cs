using Bus_IdentityUser.Interface;
using Bus_IdentityUser.Services;
using DalBackup.Data;
using DalBackup.Interface;
using DalBackup.Repository;
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
        public static async Task<IServiceCollection> serviceDescriptorsAsync(this IServiceCollection services, IConfiguration configuration)
        {

            var mailsettings = configuration.GetSection("MailSettings");
            MailSettingCreate.Email = configuration.GetValue<string>("MailSettings:Mail") ?? string.Empty;
            services.Configure<MailSettings>(mailsettings);               // đăng ký để Inject
            services.AddTransient<IEmailSender, SendMailService>();
            services.AddScoped<IBusUserManager, BusUserManager>();
            services.AddScoped<IDalUser, DalUser>();
            
      
            return services;
        }
    }
}
