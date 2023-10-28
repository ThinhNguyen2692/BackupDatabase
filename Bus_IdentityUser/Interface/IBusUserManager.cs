using Microsoft.AspNetCore.Authentication;
using ModelProject.ViewModels.ViewModelIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_IdentityUser.Interface
{
    public interface IBusUserManager
    {
        public Task<ResultLoginViewModel> Login(LoginViewModel loginViewModel);
        public Task<bool> ConfirmedEmailAsync(string UserId, string Code);
        public Task<ResultChangePassWord> ChangePassWordAsync(ChangeUserPasswordViewModel changeUserPasswordViewModel);
        public UserLoginViewModel GetUserLogin();
        public Task LogOutAsync();
    }
}
