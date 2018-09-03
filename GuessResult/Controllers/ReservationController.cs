//using GuessResult.DB.Models;
//using GuessResult.Helpers;
//using GuessResult.Models;
//using GuessResult.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Web;
//using System.Web.Mvc;

//namespace GuessResult.Controllers
//{

//    public class ReservationController : Controller
//    {
//        protected long UserId
//        {
//            get
//            {
//                return long.Parse(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value);
//            }
//        }
//        // GET: Reservation
//        public ActionResult Index()
//        {
//            return View();
//        }

//        [HttpGet]
//        [Authorize]
//        public ActionResult ReservationList()
//        {
//            try
//            {
//                return View(new ReservationListViewModel());
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return View("Error");
//            }
//        }
//        [HttpGet]
//        [Authorize]
//        public ActionResult ReservationDetails(long? id)
//        {
//            try
//            {
//                EditReservationViewModel model = new EditReservationViewModel();
//                ReservationRepository reservationRepository = new ReservationRepository();
//                if (id.HasValue == true)
//                {
//                    RRReservation pobranaRezerwacja = reservationRepository.GetById(id.Value);
//                    model.Id = pobranaRezerwacja.Id;
//                    model.Date = pobranaRezerwacja.Date;
//                    model.RRRoomId = pobranaRezerwacja.RRRoomId;
//                }
//                return View("ReservationDetails", model);
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return View("Error");
//            }
//        }



//        [HttpPost]
//        [Authorize]
//        public ActionResult SaveReservationDetails(EditReservationViewModel model)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    ReservationRepository reservationRepository = new ReservationRepository();
//                    RRReservation reservation = null;
//                    if (model.Id.HasValue)
//                    {
//                        reservation = reservationRepository.GetById(model.Id.Value);
//                    }
//                    else
//                    {
//                        reservation = new RRReservation();
//                        reservation.Status = GuessResult.Enum.ReservationStatus.InProgress;
//                    }
//                    reservation.Date = model.Date.Value;
//                    reservation.RRRoomId = model.RRRoomId.Value;
//                    reservation.RRUserId = UserId;
//                    long? rezultatZapisu = reservationRepository.Save(reservation);
//                    if (rezultatZapisu == null)
//                    {
//                        return View("Error");
//                    }
//                    else
//                    {
//                        return RedirectToAction("ReservationList");
//                    }
//                }
//                else
//                {
//                    return View("Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return View("Error");
//            }
//        }


//        [Authorize]
//        [HttpPost]
//        public ActionResult DeleteReservation(long id)
//        {
//            try
//            {
//                if (ModelState.IsValid == true)
//                {
//                    ReservationRepository reservationRepository = new ReservationRepository();
//                    bool deleteResult = reservationRepository.Delete(id);
//                    return RedirectToAction("ReservationList");
//                }
//                else
//                {
//                    return View("Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return View("Error");
//            }
//        }
//    }
//}