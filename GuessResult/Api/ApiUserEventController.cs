using GuessResult.DB.Models;
using GuessResult.Helpers;
using GuessResult.Models;
using GuessResult.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace GuessResult.Api
{
    public class ApiUserEventController : ApiController
    {
        protected long UserId
        {
            get
            {
                return long.Parse(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            }
        }

        private UserEventRepository _userEventRepository = new UserEventRepository();

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetByEventId(long eventId)
        {
            try
            {
                UserEventListItem result = new UserEventListItem();
                var eventFromDb = new EventRepository().GetById(eventId);
                result.EventId = eventId;
                result.AwayTeamName = eventFromDb.AwayTeamName;
                result.HomeTeamName = eventFromDb.HomeTeamName;
                result.StartDate = eventFromDb.StartDate;
                var userEventFromDb = _userEventRepository.GetByEventIdAndUserId(eventId, UserId);
                if (userEventFromDb != null)
                {
                    result.Id = userEventFromDb.Id;
                    result.AwayTeamScore = userEventFromDb.AwayTeamScore;
                    result.HomeTeamScore = userEventFromDb.HomeTeamScore;
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return InternalServerError();
            }
        }

        //[Authorize]
        //[HttpDelete]
        //public IHttpActionResult Delete([FromUri]long id)
        //{
        //    try
        //    {
        //        GRUserEvent userEventToDelete = _userEventRepository.GetById(id);
        //        userEventToDelete.IsDeleted = true;
        //        long? saveResult = _userEventRepository.Save(userEventToDelete);
        //        if (saveResult.HasValue)
        //        {
        //            return Ok();
        //        }
        //        else
        //        {
        //            return InternalServerError();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Log.Error(ex);
        //        return InternalServerError();
        //    }
        //}

        //http://api.football-data.org/v2/competitions
        //https://www.football-data.org/documentation/quickstart
        ///v2/matches 
        /*
         * competitions={competitionIds}
        dateFrom={DATE}
        dateTo={DATE}
        status={STATUS} 
         */

        [Authorize]
        [HttpPost]
        public IHttpActionResult SaveUserEventDetails(UserEventListItem model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserEventRepository userEventRepository = new UserEventRepository();
                    GRUserEvent userEvent = null;
                    if (model.Id.HasValue)
                    {
                        userEvent = userEventRepository.GetById(model.Id.Value);
                    }
                    else
                    {
                        userEvent = new GRUserEvent();
                    }

                    EventRepository eventRepository = new EventRepository();
                    userEvent.HomeTeamScore = model.HomeTeamScore.Value;
                    userEvent.AwayTeamScore = model.AwayTeamScore.Value;
                    model.UserId = UserId;
                    userEvent.UserId = model.UserId;
                    userEvent.EventId = model.EventId;

                    long? saveResult = userEventRepository.Save(userEvent);

                    if (saveResult == null)
                    {
                        return InternalServerError();
                    }
                    else
                    {
                        return Ok();
                    }
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return InternalServerError();
            }
        }
    }
}
