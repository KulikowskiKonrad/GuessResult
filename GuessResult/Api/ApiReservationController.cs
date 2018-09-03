//using GuessResult.DB.Models;
//using GuessResult.Extensions;
//using GuessResult.Helpers;
//using GuessResult.Models;
//using GuessResult.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Security.Claims;
//using System.Web.Http;

//namespace GuessResult.Api
//{
//    public class ApiReservationController : ApiController
//    {

//        private ReservationRepository _reservationRepository = new ReservationRepository();
//        protected long UserId
//        {
//            get
//            {
//                return long.Parse(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value);
//            }
//        }

//        [Authorize]
//        [HttpGet]
//        public IHttpActionResult GetAll(long? roomId)
//        {
//            try
//            {
//                List<ReservationListItem> result = _reservationRepository.GetAll(UserId, roomId)
//                    .Select(x => new ReservationListItem()
//                    {
//                        Date = x.Date,
//                        Id = x.Id,
//                        Status = x.Status,
//                        StatusText = x.Status.GetEnumDescription().ToString(),
//                        RoomName = x.Room.Name,
//                        UserEmail = x.User.Email,
//                        RoomId = x.RRRoomId
//                    })
//                .OrderByDescending(x => x.Date)
//                .ToList();
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return InternalServerError();
//            }
//        }

//        [Authorize]
//        [HttpDelete]
//        public IHttpActionResult Delete([FromUri]long id)
//        {
//            try
//            {
//                RRReservation reservationToDelete = _reservationRepository.GetById(id);
//                reservationToDelete.IsDeleted = true;
//                long? saveResult = _reservationRepository.Save(reservationToDelete);
//                if (saveResult.HasValue)
//                {
//                    return Ok();
//                }
//                else
//                {
//                    return InternalServerError();
//                }
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return InternalServerError();
//            }
//        }

//        [Authorize]
//        [HttpPost]
//        public IHttpActionResult SaveReservationDetails(EditReservationViewModel model)
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
//                    RRReservation existingReservation = reservationRepository.GetByIdDate(model.RRRoomId, model.Date.Value);
//                    if (existingReservation != null && existingReservation.Id != model.Id)
//                    {
//                        return BadRequest("Pomieszczenie jest już zarezerwowane!");
//                    }
//                    long? rezultatZapisu = reservationRepository.Save(reservation);
//                    if (rezultatZapisu == null)
//                    {
//                        return InternalServerError();
//                    }
//                    else
//                    {
//                        return Ok();
//                    }
//                }
//                else
//                {
//                    return InternalServerError();
//                }
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return InternalServerError();
//            }
//        }

//        [Authorize(Roles = "Admin")]
//        [HttpPost]
//        public IHttpActionResult ChangeStatus(ChangeStatus model)
//        {
//            try
//            {
//                if (ModelState.IsValid == true)
//                {
//                    ReservationRepository reservationRepository = new ReservationRepository();
//                    RRReservation reservation = reservationRepository.GetById(model.Id);
//                    reservation.Status = model.Status;
//                    reservationRepository.Save(reservation);
//                    return Ok();
//                }
//                else
//                {
//                    return InternalServerError();
//                }
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return InternalServerError();
//            }
//        }

//        [HttpGet]
//        public IHttpActionResult GetCalendarItems(long? roomId)
//        {
//            try
//            {
//                List<CalendarItem> result = _reservationRepository.GetAll(User.Identity.IsAuthenticated ? (long?)UserId : null, roomId)
//                .Where(x => x.Status != Enum.ReservationStatus.Rejected)
//                .Select(x => new CalendarItem()
//                {
//                    Date = x.Date,
//                    Title = x.Room.Name
//                })
//            .OrderByDescending(x => x.Date)
//            .ToList();
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return InternalServerError();
//            }
//        }
//    }
//}
