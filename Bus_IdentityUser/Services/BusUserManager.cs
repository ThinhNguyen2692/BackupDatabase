using Bus_IdentityUser.Interface;
using DalBackup.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using ModelProject.ViewModels.ViewModelIdentity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using DalBackup.Interface;
using ModelProject.EmailIdentity;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Bus_IdentityUser.Services
{
    public class BusUserManager : IBusUserManager
    {
        private readonly IDalUser _dalUser;
        public BusUserManager(IDalUser dalUser)
        {
            _dalUser = dalUser;
        }

        //public async Task<List<AuthenticationScheme>> ExternalLogins()
        //{
        //    var ExternalLoginsList = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        //    return ExternalLoginsList;
        //}
        public async Task<ResultLoginViewModel> Login(LoginViewModel loginViewModel)
        {
            var result = await _dalUser.SignInResult(loginViewModel.Username, loginViewModel.Password, loginViewModel.RememberMe);
           
            if (result.Succeeded == false)
            {
                //check user chưa Confirmed email

                var user = await _dalUser.FindByNameAsync(loginViewModel.Username);
                if (user != null && user.EmailConfirmed == false)
                {
                    var userId = await _dalUser.GetUserIdAsync(user);
                    var code = await _dalUser.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    result.Area = "Identity";
                    result.UserId = userId;
                    result.Code = code;
                    result.IsEmailConfirmed = false;
                }
               
            }else
            {
                if (loginViewModel.Password.Contains(MailSettingCreate.PassWordDefault))
                {
                    result.IsChangPassWord = true;
                }
            }
            return result;
        }

        public async Task<bool> ConfirmedEmailAsync(string UserId, string Code)
        {
            var user = await _dalUser.FindByIdAsync(UserId);
            if (user == null)
            {
                return false;
            }
            Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Code));
            var result = await _dalUser.ConfirmEmailAsync(user, Code);
            return result;
        }

        public async Task<ResultChangePassWord> ChangePassWordAsync(ChangeUserPasswordViewModel changeUserPasswordViewModel)
        {
            var user = await _dalUser.FindByIdAsync(changeUserPasswordViewModel.UserId);
            var result = new ResultChangePassWord();
            if (user != null)
            {
                result = await _dalUser.ChangePassWordAsync(user, changeUserPasswordViewModel.CurrentPassword, changeUserPasswordViewModel.NewPassword);
            }
            return result;
        }

        public UserLoginViewModel GetUserLogin()
        {
            var result = _dalUser.GetUserLogin();
            var UserLoginViewModel = new UserLoginViewModel();
            if(result != null)
            {
                UserLoginViewModel.Id = result.Id;
                UserLoginViewModel.UserName = result.UserName;
                UserLoginViewModel.Email = result.Email;
            }
            return UserLoginViewModel;
        }

        public async Task LogOutAsync()
        {
            await _dalUser.LogOutAsync();
        }

    }
}
