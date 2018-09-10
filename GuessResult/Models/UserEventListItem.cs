using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuessResult.Models
{
    public class UserEventListItem
    {
        public long? Id { get; set; }

        public long UserId { get; set; }

        public long EventId { get; set; }

        [Required]
        public byte? HomeTeamScore { get; set; }

        [Required]
        public byte? AwayTeamScore { get; set; }

        //dodac nazwy zespolow 
        public string AwayTeamName { get; set; }

        public string HomeTeamName { get; set; }

        public DateTime StartDate { get; set; }
    }
}