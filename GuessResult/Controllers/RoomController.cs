//using GuessResult.DB.Models;
//using GuessResult.Helpers;
//using GuessResult.Models;
//using GuessResult.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace GuessResult.Controllers
//{
//    [Authorize(Roles = "Admin")]
//    public class RoomController : Controller
//    {
//        [HttpGet]
//        public ActionResult RoomList()
//        {
//            try
//            {
//                return View("RoomList");
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return View("Error");
//            }
//        }
//    }
//}