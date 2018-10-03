using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessResult.DB.Models
{
    public class GRNewsFeedLike
    {

        public long Id { get; set; }

        public long GRNewsFeedId { get; set; }

        public long GRUserId { get; set; }

        public long GRNewsFeedCommentId { get; set; }



        public virtual GRUser User { get; set; }

        public virtual GRNewsFeed NewsFeed { get; set; }

        public virtual GRNewsFeedComment NewsFeedComment { get; set; }
    }
}