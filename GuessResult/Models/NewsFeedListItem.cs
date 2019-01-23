using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuessResult.Models
{
    public class NewsFeedListItem
    {
        public long? Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int LikeCount { get; set; }

        public int CommentCount { get; set; }


        public DateTime InsertDate { get; set; }

        public string InsertUserEmail { get; set; }

        public bool IsLikedByCurrentUser { get; set; }
    }
}