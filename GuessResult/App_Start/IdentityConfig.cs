using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using GuessResult.DB.Models;
using GuessResult.Enum;
using GuessResult.Models;
using GuessResult.Repositories;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.SessionState;

public class ApplicationSignInManager
{
    private HttpSessionState Session
    {
        get
        {
            return HttpContext.Current.Session;
        }
    }
    private IOwinContext OwinContext { get; set; }
    public ApplicationSignInManager(IOwinContext owinContext)
    {
        OwinContext = owinContext;
    }

    public SignInStatus PasswordSignIn(LoginViewModel model)
    {
        SignInStatus result = SignInStatus.Failure;

        GRUser user = new UserRepository().GetByEmailPassword(model.Email, model.Password);
        if (user != null)
        {
            result = SignInStatus.Success;
            var ident = new ClaimsIdentity(
              new[] {
                      new Claim(ClaimTypes.NameIdentifier, model.Email),
                      new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                      new Claim(ClaimTypes.Name, model.Email),
                      new Claim(ClaimTypes.Role, ((UserRole)user.Role).ToString()),
                      new Claim("UserId", user.Id.ToString())
              },
              DefaultAuthenticationTypes.ApplicationCookie);

            OwinContext.Authentication.SignIn(new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = model.RememberMe ? (DateTime?)DateTime.UtcNow.AddDays(7) : null // jezeli wybrano zapamietaj mnie to uzytkownik bedzie zalogowany przez 7 dni
            }, ident);
            result = SignInStatus.Success;
        }

        return result;
    }

    public SignInStatus ExternalLogin(string login, string userName)
    {
        SignInStatus result = SignInStatus.Failure;

        UserRepository userRepository = new UserRepository();

        GRUser user = userRepository.GetByLogin(login);
        if (user == null)
        {
            user = new GRUser()
            {
                Email = login,
                UserName = !string.IsNullOrEmpty(userName) ? userName : login,
                //Password = "test",
                Role = UserRole.Standard,
                //Salt = "test"
            };
            long? userSaveResult = userRepository.Save(user);
            if (!userSaveResult.HasValue)
            {
                return SignInStatus.Failure;
            }
        }

        result = SignInStatus.Success;
        var ident = new ClaimsIdentity(
          new[] {
                      new Claim(ClaimTypes.NameIdentifier,login),
                      new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                      new Claim(ClaimTypes.Name,login),
                      new Claim(ClaimTypes.Role, ((UserRole)user.Role).ToString()),
                      new Claim("UserId", user.Id.ToString())
          },
          DefaultAuthenticationTypes.ApplicationCookie);

        OwinContext.Authentication.SignIn(new AuthenticationProperties
        {
            IsPersistent = true,
        }, ident);
        result = SignInStatus.Success;

        return result;
    }
}