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
    public class ApiNewsFeedCommentController : ApiController
    {

        protected long UserId
        {
            get
            {
                return long.Parse(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            }
        }

        private INewsFeedCommentRepository _newsFeedCommentRepository;

        public ApiNewsFeedCommentController()
        {

        }

        public ApiNewsFeedCommentController(INewsFeedCommentRepository newsFeedCommentRepository)
        {
            _newsFeedCommentRepository = newsFeedCommentRepository;
        }


        [Authorize]
        [HttpPost]
        public IHttpActionResult SaveNewsFeedComment([FromBody]SaveNewsFeedCommentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    long? saveResult = _newsFeedCommentRepository.Save(model, UserId);
                    if (saveResult == null)
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
        [HttpGet]
        public IHttpActionResult GetNewsFeedComments([FromUri]long newsFeedId)
        {
            try
            {
                List<NewsFeedCommentListItem> result = _newsFeedCommentRepository.GetAllNewsFeedComment(newsFeedId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return InternalServerError();
            }
        }

        //[Authorize]
        //[HttpPost]
        //public IHttpActionResult Like([FromBody]NewsFeedLikeViewModel model)
        //{
        //    try
        //    {
        //        bool likeResult = _iNewsFeedRepository.Like(model.Id);
        //        if (likeResult == false)
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
