using AdminLayout_Vuexy.Models;
using Bus_backUpData.Interface;
using Bus_IdentityUser.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelProject.EmailIdentity;
using ModelProject.Models;
using ModelProject.ViewModels.ViewModelIdentity;
using System.Diagnostics;
using System.Text.Encodings.Web;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace AdminLayout_Vuexy.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusUserManager _busUserManager;
        private readonly IEmailSender _emailSender;

        public LoginController(ILogger<HomeController> logger, IBusUserManager _busUserManager, IEmailSender emailSender)
        {
            _logger = logger;
            this._busUserManager = _busUserManager;
            _emailSender = emailSender;
        }
        [HttpGet]
        [Route("/Login")]
        public IActionResult Index(string returnUrl = null)
        {
            var ReturnUrl = returnUrl ?? "/";
            var viewModel = new LoginViewModel() { ReturnUrl = ReturnUrl };
            return View("Index", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Login")]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _busUserManager.Login(loginViewModel);
                if (!result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    if (!result.IsEmailConfirmed)
                    {
                        var callbackUrl = Url.ActionLink("ConfirmedEmail","Login", new { userId = result.UserId, code = result.Code}) ?? string.Empty;
                        await _emailSender.SendEmailAsync(MailSettingCreate.Email, "Confirm your email",
                      $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                        return LocalRedirect("CheckMail");
                    }
                }
                else
                {
                    if(result.IsChangPassWord == true)
                    {
                        return LocalRedirect("/ChangePassword");
                    }
                    return LocalRedirect(loginViewModel.ReturnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = loginViewModel.ReturnUrl, RememberMe = loginViewModel.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    ModelState.AddModelError(string.Empty, "User account locked out.");
                    return View();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
            }
                return View();
        }
        [HttpGet]
        [Route("/CheckMail")]
        public IActionResult CheckMail()
        {
            var ViewModel = new ConfirmedEmailViewModel();
            ViewModel.Message = "Check email";
            return View(ViewModel);
        }
        [HttpGet]
        [Authorize]
        [Route("/ChangePassword")]
        public IActionResult ChangePassword()
        {
            var viewModel = new ChangeUserPasswordViewModel();
            var UserLogin = _busUserManager.GetUserLogin();
            viewModel.UserId = UserLogin.Id;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Route("/ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordViewModel changeUserPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _busUserManager.ChangePassWordAsync(changeUserPasswordViewModel);
                if (!result.Succeeded)
                {
                    foreach (var Error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, Error);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "ChangePassword Succeeded");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "ChangePassword Succeeded Error");
            }
            return View(changeUserPasswordViewModel);
        }       
        [HttpGet]
        [Route("/ConfirmedEmail")]
        public async Task<IActionResult> ConfirmedEmail(string userId, string code)
        {
            var confirmedEmail = await _busUserManager.ConfirmedEmailAsync(userId, code);
            var ViewModel = new ConfirmedEmailViewModel();
            ViewModel.Message = confirmedEmail ? "Thank you for confirming your email." : "Error confirming your email.";
            return View("CheckMail", ViewModel);
        }

        [HttpGet]
        [Route("/logout")]
        public async Task<IActionResult> LogOut(string returnUrl = null)
        {
            await _busUserManager.LogOutAsync();
            returnUrl = returnUrl != null ? "/"+returnUrl :  "/";
            return LocalRedirect(returnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
