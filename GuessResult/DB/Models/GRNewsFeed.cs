using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuessResult.DB.Models
{
    public class GRNewsFeed
    {
        public long Id { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public int LikeCount { get; set; }

        public DateTime InsertDate { get; set; }

        public long GRUserId { get; set; }

        public virtual GRUser User { get; set; }
    }
}