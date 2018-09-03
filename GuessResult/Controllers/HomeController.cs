using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuessResult.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "User");
            //if (User.Identity.IsAuthenticated == true)
            //{
            //    return RedirectToAction("Index", "User");
            //}
            //else
            //{
            //    return View();
            //}
        }
    }
}