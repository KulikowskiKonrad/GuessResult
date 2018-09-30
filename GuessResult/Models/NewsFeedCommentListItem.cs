using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessResult.Models
{
    public class NewsFeedCommentListItem
    {
        public long Id { get; set; }

        public string Content { get; set; }

        public DateTime InsertDate { get; set; }

        public string InsertUserEmail { get; set; }
    }
}