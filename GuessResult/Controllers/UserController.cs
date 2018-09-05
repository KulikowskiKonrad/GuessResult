using GuessResult.Helpers;
using GuessResult.Models;
using GuessResult.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using GuessResult.DB.Models;
using GuessResult.Enum;
using System.Net;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;
using static GuessResult.Startup;
using System.Threading.Tasks;

namespace GuessResult.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                UserRepository uzytkownikRepozytorium = new UserRepository();
                return View("Login");
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)

        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var result = new ApplicationSignInManager(HttpContext.GetOwinContext()).PasswordSignIn(model);

                    switch (result)
                    {
                        case SignInStatus.Success:
                            return RedirectToAction("Index", "Home");

                        case SignInStatus.LockedOut:
                        case SignInStatus.RequiresVerification:
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("Haslo", "Niepoprawny login lub hasło");
                            return View("Login", model);
                    }

                }
                else
                {
                    return View("Login", model);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }

        }
        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    if (Request.IsAuthenticated == true)
                    {
                        Session.Abandon();
                        HttpContext.GetOwinContext().Authentication.SignOut();
                    }
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }


        [HttpPost]
        public ActionResult ExternalLogin(string provider)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback"));
        }

        public async Task<ActionResult> ExternalLoginCallback()
        {
            var loginInfo = await HttpContext.GetOwinContext().Authentication.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Index","Home");
            }


            var result = new ApplicationSignInManager(HttpContext.GetOwinContext()).ExternalLogin(loginInfo.Email, loginInfo.DefaultUserName);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");

                case SignInStatus.LockedOut:
                case SignInStatus.RequiresVerification:
                case SignInStatus.Failure:
                default:
                    return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
    }
}