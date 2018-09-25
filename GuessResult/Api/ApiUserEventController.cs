using GuessResult.DB.Models;
using GuessResult.Helpers;
using GuessResult.Models;
using GuessResult.Repositories;
using GuessResult.Repositories.Interfaces;
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

        private IUserEventRepository _userEventRepository;
        private IEventRepository _eventRepository;


        public ApiUserEventController()
        {

        }

        public ApiUserEventController(IUserEventRepository userEventRepository, IEventRepository eventRepository)
        {
            _userEventRepository = userEventRepository;
            _eventRepository = eventRepository;
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetByEventId(long eventId)
        {
            try
            {
                UserEventListItem result = _eventRepository.GetUserEventListItemById(eventId, UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult SaveUserEventDetails(UserEventListItem model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //UserEventRepository userEventRepository = new UserEventRepository();

                    long? saveResult = _userEventRepository.Save(model, UserId, User.IsInRole("Admin"));
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

                //GRUserEvent userEvent = null;
                //    if (model.Id.HasValue)
                //    {
                //        userEvent = userEventRepository.GetById(model.Id.Value);
                //    }
                //    else
                //    {
                //        userEvent = new GRUserEvent();
                //    }

                //    EventRepository eventRepository = new EventRepository();
                //    var eventFromDb = eventRepository.GetById(model.EventId);

                //    if (User.IsInRole("Admin") && model.EventPredictionType.HasValue)
                //    {
                //        eventFromDb.PredictionType = model.EventPredictionType.Value;
                //        eventRepository.Save(eventFromDb);
                //    }

                //    if (eventFromDb.PredictionType == Enum.EventPredictionType.ExactScore)
                //    {
                //        userEvent.HomeTeamScore = model.HomeTeamScore.Value;
                //        userEvent.AwayTeamScore = model.AwayTeamScore.Value;
                //        userEvent.GeneralScoreType = null;
                //    }
                //    else
                //    {
                //        userEvent.HomeTeamScore = null;
                //        userEvent.AwayTeamScore = null;
                //        userEvent.GeneralScoreType = model.GeneralScoreType;
                //    }

                //    model.UserId = UserId;
                //    userEvent.UserId = model.UserId;
                //    userEvent.EventId = model.EventId;

                //    long? saveResult = userEventRepository.Save(userEvent);

                //    if (saveResult == null)
                //    {
                //        return InternalServerError();
                //    }
                //    else
                //    {
                //        return Ok();
                //    }
                //}
                //else
                //{
                //    return InternalServerError();
                //}
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return InternalServerError();
            }
        }
    }
}
