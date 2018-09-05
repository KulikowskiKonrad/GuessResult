using GuessResult.DB.Models;
using GuessResult.Helpers;
using GuessResult.Models;
using GuessResult.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GuessResult.Api
{
    public class ApiEventController : ApiController
    {

        private EventRepository _eventRepository = new EventRepository();

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                List<EventListItem> result = _eventRepository.GetAll()
                    .Select(x => new EventListItem()
                    {
                        AwayTeamName = x.AwayTeamName,
                        HomeTeamName = x.HomeTeamName,
                        Id = x.Id,
                        AwayTeamScore= x.AwayTeamScore,
                        HomeTeamScore=x.HomeTeamScore,
                        StartDate = x.StartDate
                    })
                .OrderByDescending(x => x.StartDate)
                .ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri]long id)
        {
            try
            {
                GREvent eventToDelete = _eventRepository.GetById(id);
                eventToDelete.IsDeleted = true;
                long? saveResult = _eventRepository.Save(eventToDelete);
                if (saveResult.HasValue)
                {
                    return Ok();
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


        [Authorize]
        [HttpPost]
        public IHttpActionResult SaveEventDetails(EditEventViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EventRepository eventRepository = new EventRepository();
                    GREvent singleEvent = null;
                    if (model.Id.HasValue)
                    {
                        singleEvent = eventRepository.GetById(model.Id.Value);
                    }
                    else
                    {
                        singleEvent = new GREvent();
                    }

                    singleEvent.StartDate = model.StartDate;
                    singleEvent.HomeTeamName = model.HomeTeamName;
                    singleEvent.AwayTeamName = model.AwayTeamName;
                    singleEvent.HomeTeamScore = model.HomeTeamScore;
                    singleEvent.AwayTeamScore = model.AwayTeamScore;

                    long? saveResult = eventRepository.Save(singleEvent);

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
