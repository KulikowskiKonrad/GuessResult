using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessResult.Models
{
    public class SaveNewsFeedCommentViewModel
    {
        public long? Id { get; set; }

        public long NewsFeedId { get; set; }

        public string Content { get; set; }
    }
}