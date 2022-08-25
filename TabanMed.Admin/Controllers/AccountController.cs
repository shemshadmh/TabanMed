using Application;
using Application.Common;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Resources.ErrorMessages;
using TabanAgency.Domain.Dtos.SystemUsers;

namespace TabanMed.Admin.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpGet("login")]
    public IActionResult Login()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Dashboard");
        }

        return View();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginSystemUserDto loginDto, string response, string? returnUrl)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                    .Where(v => v.ValidationState == ModelValidationState.Invalid)
                    .Select(v => v.Errors.Select(e => e.ErrorMessage).First())
                    .First();
                TempDataMessage(errorMessage);
                return View(loginDto);
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = await client.PostAsync(
                    $"https://www.google.com/recaptcha/api/siteverify?secret=6LdQ9cIeAAAAAE9rlB4AW3N_8dlK-sF2L5z0z-K8&response=" +
                    response, null);
                ReCaptchaResponse? reCaptchaResponse =
                    System.Text.Json.JsonSerializer.Deserialize<ReCaptchaResponse>(
                        await result.Content.ReadAsStringAsync());

                if (!reCaptchaResponse!.Success)
                {
                    ModelState.AddModelError("",
                        "ترافیک دریافت شده از جانب شما خطرناک شناسایی شده است. لطفا مجدد تلاش کنید");

                    _logger.LogWarning(
                        $"username[{loginDto.UserName.Substring(0, 5)}****] rejected by captcha. IP[{Request.HttpContext.Connection.RemoteIpAddress}]");
                    return View(loginDto);
                }
            }

            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            // not found but don't say that to client
            if (user != null)
            {
                // sign in with password
                //var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
                // check the password
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);

                if (result.Succeeded)
                {
                    user.LastModified = DateTime.UtcNow;
                    await _userManager.UpdateSecurityStampAsync(user);

                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MinValue);
                    await _userManager.ResetAccessFailedCountAsync(user);

                    // sign in validated user and append the system user timezone to claims.
                    await _signInManager.SignInAsync(user, loginDto.RememberMe);

                    _logger.LogWarning(
                        $"username[{loginDto.UserName.Substring(0, 5)}****] logged in successfully. IP[{Request.HttpContext.Connection.RemoteIpAddress}]");

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        if (Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        TempDataMessage(ErrorMessages.YouAreLimitedForATime);
                        return View(loginDto);
                    }
                    else if (result.IsNotAllowed)
                    {
                        TempDataMessage(ErrorMessages.AccessDeniedOrLimited);
                        return View(loginDto);
                    }
                }
            }

            _logger.LogWarning(
                $"username[{loginDto.UserName.Substring(0, 5)}****] faild by Wrong UserName Or Password. IP[{Request.HttpContext.Connection.RemoteIpAddress}]");
            TempDataMessage(ErrorMessages.WrongUserNameOrPassword);
            return View(loginDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                $"Couldn't authorize username[{loginDto.UserName.Substring(0, 5)}****] caused errors");
            TempDataMessage(ErrorMessages.ErrorOccurred);
            return View(loginDto);
        }
    }

    [AllowAnonymous]
    [HttpGet("accessDenied")]
    public IActionResult AccessDenied() => View();

    [HttpPost("logout"), Authorize]
    public async Task<IActionResult> Logout(string? returnUrl = null)
    {
        await _signInManager.SignOutAsync();
        if (returnUrl != null)
            return LocalRedirect(returnUrl);
        return RedirectToAction(nameof(Login));
    }

    public void TempDataMessage(string message, bool isSuccessful = false)
    {
        TempData[AppConstants.TempDataMessageTitle] = $"{message}~{isSuccessful}";
    }
}