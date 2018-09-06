using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuessResult.Models
{
    public class EditUserEventViewModel
    {
        public long? Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public byte? AwayTeamScore { get; set; }

        public byte? HomeTeamScore { get; set; }

        public long UserId { get; set; }

        public long EventId { get; set; }


    }
}