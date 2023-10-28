using Dal_IdentityUser.Data;
using Microsoft.AspNetCore.Identity;
using ModelProject.ViewModels.ViewModelIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_IdentityUser.Interface
{
    public interface IDalUser
    {
        public Task<ResultLoginViewModel> SignInResult(string Email, string Password, bool RememberMe);
        public Task<AdminLayout_VuexyUser> FindByNameAsync(string name);
        public Task<string> GetUserIdAsync(AdminLayout_VuexyUser user);
        public Task<string> GenerateEmailConfirmationTokenAsync(AdminLayout_VuexyUser user);
        public Task<bool> ConfirmEmailAsync(AdminLayout_VuexyUser user, string Code);
        public Task<AdminLayout_VuexyUser> FindByIdAsync(string UserId);
        public Task<ResultChangePassWord> ChangePassWordAsync(AdminLayout_VuexyUser user, string CurrentPassword, string NewPassword);
        public AdminLayout_VuexyUser? GetUserLogin();
        public Task LogOutAsync();
    }
}
