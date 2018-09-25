using GuessResult.DB.Models;
using GuessResult.Enum;
using GuessResult.Helpers;
using GuessResult.Models;
using GuessResult.Repositories;
using GuessResult.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace GuessResult.Api
{
    public class ApiEventController : ApiController
    {

        protected long UserId
        {
            get
            {
                return long.Parse(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            }
        }
        private IEventRepository _eventRepository;

        public ApiEventController()
        {

        }


        public ApiEventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetAll([FromUri]EventStatus? filterEventStatus, [FromUri]bool filterOnlyMyEvents)
        {
            try
            {
                List<EventListItem> result = _eventRepository.GetAllEventListItems(filterEventStatus, filterOnlyMyEvents, UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return InternalServerError();
            }
        }



        [Authorize]
        [HttpGet]
        public IHttpActionResult GetUserEffectivityData(GeneralScoreType? effectivityFilter = null)
        {
            try
            {
                List<ChartDataItem> result = _eventRepository.GetEffectivityData(UserId, effectivityFilter);

                return Ok(result);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetTopUsersData()
        {
            try
            {
                List<ChartDataItem> result = _eventRepository.GetTopUsersData();

                return Ok(result);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return InternalServerError();
            }
        }
    }
}
