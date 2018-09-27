using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuessResult.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View("TestView");
        }
    }
}