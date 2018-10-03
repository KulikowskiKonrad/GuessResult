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
        [HttpGet]
        public IHttpActionResult GetAllNewsFeedComment()
        {
            try
            {
                List<NewsFeedCommentListItem> result = _newsFeedCommentRepository.GetAllNewsFeedComment();
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
