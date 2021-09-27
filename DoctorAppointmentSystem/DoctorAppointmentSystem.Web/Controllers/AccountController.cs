using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ExtendedIdentityUser> _signInManager;
        private readonly UserManager<ExtendedIdentityUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        // private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager<ExtendedIdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ExtendedIdentityUser> signInManager,
            ILogger<AccountController> logger)
        //  IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            var model = new LoginModel();

            returnUrl = returnUrl ?? Url.Content("~/");
            ViewBag.ReturnUrl = returnUrl;

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            model.ReturnUrl = returnUrl;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model,string returnUrl)
        {
            model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/admin");
            // var Identityuser =await _userManager.FindByEmailAsync(model.Email);


            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    await _userManager.UpdateAsync(user);

                    _logger.LogInformation("User logged in.");
                    ViewBag.Message = "Success";
                    if(await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                       return LocalRedirect("/Admin/dashboard");
                    }
                    else if(await _userManager.IsInRoleAsync(user, "Doctor"))
                    {
                        return LocalRedirect("/Admin/Appointment/index");
                    }
                    else return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    ViewBag.Message = "Invalid Login Attempt. Wrong Confirmation Password";
                    return View(model);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            var model = new RegisterModel();
            //model.Roles = (from r in _roleManager.Roles
            //               select new SelectListItem
            //               {
            //                   Value = r.Id,
            //                   Text = r.Name
            //               }).ToList();


            model.ReturnUrl = returnUrl;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/");
           
                var user = new ExtendedIdentityUser { UserName = model.Email,Name=model.Name,Email=model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    // var role = await _roleManager.FindByIdAsync(model.Role);
                    // var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action(
                    //    "ConfirmEmail", "Account",
                    //    values: new { userId = user.Id, code = code },
                    //    protocol: Request.Scheme); ;

                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: true);
                    return LocalRedirect(model.ReturnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return LocalRedirect("/");
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View(model);
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                //  code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Content("/Account/ResetPassword?code=" + code + "&email=" + model.Email);

                //await _emailSender.SendEmailAsync(
                //    model.Email,
                //    "Reset Password",
                //    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return LocalRedirect(callbackUrl);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string code = null, string email = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                var model = new ResetPasswordModel
                {
                    Code = code,
                    Email = email
                };
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            model.Code = model.Code.Replace(" ", "+");
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return View(model);
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
