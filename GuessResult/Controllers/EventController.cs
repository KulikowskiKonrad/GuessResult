using GuessResult.DB.Models;
using GuessResult.Helpers;
using GuessResult.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuessResult.Controllers
{
    public class EventController : Controller
    {
        public ActionResult EventList()
        {
            try
            {
                return View();

            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }

        }
        public ActionResult PredictionResult()
        {
            try
            {
                return View();

            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }

        }
    }
}