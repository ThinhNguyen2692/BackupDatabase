using Dal_IdentityUser.Data;
using Dal_IdentityUser.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using ModelProject.ViewModels.ViewModelIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_IdentityUser.Services
{
    public class DalUser : IDalUser
    {
        private readonly SignInManager<AdminLayout_VuexyUser> _signInManager;
        private readonly UserManager<AdminLayout_VuexyUser> _userManager;
        public DalUser(SignInManager<AdminLayout_VuexyUser> signInManager, UserManager<AdminLayout_VuexyUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<ResultLoginViewModel> SignInResult(string Email, string Password, bool RememberMe)
        {
          var Result = await _signInManager.PasswordSignInAsync(Email,Password, RememberMe, lockoutOnFailure: false);
            var resultViewModel = new ResultLoginViewModel();
            resultViewModel.Succeeded = Result.Succeeded;
            resultViewModel.RequiresTwoFactor = Result.RequiresTwoFactor;
            resultViewModel.IsLockedOut = Result.IsLockedOut;
            resultViewModel.IsEmailConfirmed = _userManager.Options.SignIn.RequireConfirmedAccount;
            return resultViewModel;
        }
        public async Task<AdminLayout_VuexyUser> FindByNameAsync(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            return user;
        }

        public async Task<AdminLayout_VuexyUser> FindByIdAsync(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            return user;
        }

        public async Task<string> GetUserIdAsync(AdminLayout_VuexyUser user)
        {
           return await _userManager.GetUserIdAsync(user);
        }
        public async Task<string> GenerateEmailConfirmationTokenAsync(AdminLayout_VuexyUser user)
        {
           return  await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<bool> ConfirmEmailAsync(AdminLayout_VuexyUser user, string Code)
        {
            var result = await _userManager.ConfirmEmailAsync(user, Code);
            return result.Succeeded;
        }
        public async Task<ResultChangePassWord> ChangePassWordAsync(AdminLayout_VuexyUser user, string CurrentPassword, string NewPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);
            var ResultChangePassWordViewModel = new ResultChangePassWord();
            ResultChangePassWordViewModel.Succeeded = result.Succeeded;
            foreach(IdentityError error in result.Errors)
            {
                ResultChangePassWordViewModel.Errors.Add(error.Description);
            }
            return ResultChangePassWordViewModel;
        }

        public AdminLayout_VuexyUser? GetUserLogin()
        {
            var result =  _userManager.Users;
            return result.FirstOrDefault();
        }
        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
