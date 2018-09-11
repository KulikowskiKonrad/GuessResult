using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuessResult.Models
{
    public class EventListItem
    {

        public long Id { get; set; }

        public long UserId { get; set; }

        public DateTime StartDate { get; set; }

        [StringLength(100)]
        [Required]
        public string HomeTeamName { get; set; }

        [StringLength(100)]
        [Required]
        public string AwayTeamName { get; set; }

        public byte? AwayTeamScore { get; set; }

        public byte? HomeTeamScore { get; set; }

        public byte? UserAwayTeamScore { get; set; }

        public byte? UserHomeTeamScore { get; set; }

    }
}