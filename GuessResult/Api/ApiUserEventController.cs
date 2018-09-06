using GuessResult.DB.Models;
using GuessResult.Helpers;
using GuessResult.Models;
using GuessResult.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuessResult.Api
{
    public class ApiUserEventController : ApiController
    {

        private UserEventRepository _userEventRepository = new UserEventRepository();

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                List<UserEventListItem> result = _userEventRepository.GetAll()
                    .Select(x => new UserEventListItem()
                    {
                        Id = x.Id,
                        AwayTeamScore = x.AwayTeamScore,
                        HomeTeamScore = x.HomeTeamScore,
                      EventId=x.EventId,
                      UserId=x.UserId
                    })
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
                GRUserEvent userEventToDelete = _userEventRepository.GetById(id);
                userEventToDelete.IsDeleted = true;
                long? saveResult = _userEventRepository.Save(userEventToDelete);
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
        public IHttpActionResult SaveUserEventDetails(EditUserEventViewModel model)
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

                    userEvent.HomeTeamScore = model.HomeTeamScore.Value;
                    userEvent.AwayTeamScore = model.AwayTeamScore.Value;
                    userEvent.EventId = model.EventId;
                    userEvent.UserId = model.UserId;

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
