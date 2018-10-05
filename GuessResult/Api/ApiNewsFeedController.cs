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
                List<NewsFeedListItem> result = _iNewsFeedRepository.GetAllNewsFeedListItems(UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return InternalServerError();
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        public IHttpActionResult Remove(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool deleteResult = _iNewsFeedRepository.Delete(id);
                    if (deleteResult == false)
                    {
                        return InternalServerError();
                    }
                    else
                    {
                        return Ok(true);
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

        [Authorize]
        [HttpPost]
        public IHttpActionResult SaveNewsFeed(SaveNewsFeedViewModel model)
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


        [HttpPost]
        public IHttpActionResult Like([FromBody]NewsFeedLikeViewModel model)
        {
            try
            {
                bool likeResult = _iNewsFeedRepository.Like(model.Id, UserId);
                if (likeResult == false)
                {
                    return InternalServerError();
                }
                else
                {
                    return Ok(true);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return InternalServerError();
            }
        }

        //[HttpPost]
        //public IHttpActionResult Comment([FromBody]NewsFeedLikeViewModel model)
        //{
        //    try
        //    {
        //        bool commentResult = _iNewsFeedRepository.Comment(model.Id, UserId);
        //        if (commentResult == false)
        //        {
        //            return InternalServerError();
        //        }
        //        else
        //        {
        //            return Ok(true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Log.Error(ex);
        //        return InternalServerError();
        //    }
        //}

    }
}
