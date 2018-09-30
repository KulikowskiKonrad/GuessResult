using GuessResult.Helpers;
using GuessResult.Models;
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
    public class ApiNewsFeedController : ApiController
    {
        protected long UserId
        {
            get
            {
                return long.Parse(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            }
        }

        private INewsFeedRepository _iNewsFeedRepository;

        public ApiNewsFeedController()
        {

        }

        public ApiNewsFeedController(INewsFeedRepository newsFeedRepository)
        {
            _iNewsFeedRepository = newsFeedRepository;
        }


        [Authorize]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                List<NewsFeedListItem> result = _iNewsFeedRepository.GetAllNewsFeedListItems();
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
        public IHttpActionResult SaveNewsFeed(NewsFeedListItem model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    long? saveResult = _iNewsFeedRepository.Save(model, UserId);
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
