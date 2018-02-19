using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Models;
using Microsoft.Extensions.Logging;
using Municipality.ViewModels;
using System.Linq;
using Repositories;
using Microsoft.AspNetCore.Http;

namespace Municipality.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    #region Fields
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IIncidentRepository _incidentsRepository;

    #endregion
    #region Constructors
    public AccountController(

       SignInManager<ApplicationUser> signInManager,
       UserManager<ApplicationUser> userManager,
       IIncidentRepository incidentsRepository
      )
    {

      _signInManager = signInManager;
      _userManager = userManager;
      _incidentsRepository = incidentsRepository;


    }
    #endregion
    #region Public methods
    [TempData]
    public string ErrorMessage { get; set; }

    [HttpGet("api/sign-in/")]
    [AllowAnonymous]
    public async Task<IActionResult> Signin(string returnUrl = null)
    {
      // Clear the existing external cookie to ensure a clean login process
      await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

      ViewData["ReturnUrl"] = returnUrl;
      return View();
    }

    [HttpPost("api/sign-in/")]
    [AllowAnonymous]
    public async Task<IActionResult> Signin([FromBody]SigninViewModel model)
    {
      StatusCodeResult res = null;
      if (ModelState.IsValid)
      {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
        if (result.Succeeded)
        {
          res = Ok();
          return await Task.FromResult(res);
        }
        else
        {
          res = NotFound();
          ModelState.AddModelError(string.Empty, "Invalid login attempt.");
          Response.StatusCode = StatusCodes.Status409Conflict;
          return Json(ModelState.Values.SelectMany(v => v.Errors).ToList());

        }
      }

                Response.StatusCode = StatusCodes.Status409Conflict;
          return Json(ModelState.Values.SelectMany(v => v.Errors).ToList());

    }

    [HttpPost("api/sign-out/")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Signout()
    {
      await _signInManager.SignOutAsync();
      StatusCodeResult res = null;

      res = Ok();
      return await Task.FromResult(res);

    }

    [HttpGet("api/sign-up/")]
    [AllowAnonymous]
    public IActionResult Signup(string returnUrl = null)
    {
      ViewData["ReturnUrl"] = returnUrl;
      return View();
    }

    [HttpPost("api/sign-up/")]
    [Produces("application/json")]
    [AllowAnonymous]
    public async Task<IActionResult> Signup([FromBody] SignupViewModel model)
    {
      StatusCodeResult res = null;
      if (ModelState.IsValid)
      {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          res = Ok();
          return await Task.FromResult(res);
        }
        else
        {
          AddErrors(result);
          Response.StatusCode = StatusCodes.Status409Conflict;
          return Json(ModelState.Values.SelectMany(v => v.Errors).ToList());
        }
      }

      Response.StatusCode = StatusCodes.Status409Conflict;
      return Json(ModelState.Values.SelectMany(v => v.Errors).ToList());

    }


    [HttpGet("/confirm-email/")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
      if (userId == null || code == null)
      {
        return RedirectToAction(nameof(HomeController.Index), "Home");
      }
      var user = await _userManager.FindByIdAsync(userId);
      if (user == null)
      {
        throw new ApplicationException($"Unable to load user with ID '{userId}'.");
      }
      var result = await _userManager.ConfirmEmailAsync(user, code);
      return View(result.Succeeded ? "ConfirmEmail" : "Error");
    }


    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPassword(string code = null)
    {
      if (code == null)
      {
        throw new ApplicationException("A code must be supplied for password reset.");
      }
      var model = new ResetPasswordViewModel { Code = code };
      return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      var user = await _userManager.FindByEmailAsync(model.Email);
      if (user == null)
      {
        // Don't reveal that the user does not exist
        return RedirectToAction(nameof(ResetPasswordConfirmation));
      }
      var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
      if (result.Succeeded)
      {
        return RedirectToAction(nameof(ResetPasswordConfirmation));
      }
      AddErrors(result);
      return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPasswordConfirmation()
    {
      return View();
    }


    [HttpGet("/forgot-password/")]
    [AllowAnonymous]
    public IActionResult ForgotPassword()
    {
      return View();
    }



    [HttpGet("/access-denied/")]
    public IActionResult AccessDenied() => View();

    [HttpGet("api/account/authorized-user")]
    [Produces("application/json")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAuthorizedUser()
    {

      var res = _incidentsRepository.All().ToList();
      return Json(
                      new
                      {
                        User = "Andrey Gavrilovych"
                      }
                  );

    }
    #endregion
    #region Helpers

    private void AddErrors(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      else
      {
        return RedirectToAction("Index", "Public");
      }
    }

    #endregion
  }
}
