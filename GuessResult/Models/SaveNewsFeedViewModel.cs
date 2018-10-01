using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuessResult.Models
{
    public class SaveNewsFeedViewModel
    {
        public long? Id { get; set; }

        [Required]
        public string Content { get; set; }
    }
}