using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessResult.DB.Models
{
    public class GRNewsFeedComment
    {
        public long Id { get; set; }

        public int CountLike { get; set; }

        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime InsertDate { get; set; }

        public long GRNewsFeedId { get; set; }

        public long GRUserId { get; set; }

        public virtual GRUser User { get; set; }

        public virtual GRNewsFeed NewsFeed { get; set; }
    }
}