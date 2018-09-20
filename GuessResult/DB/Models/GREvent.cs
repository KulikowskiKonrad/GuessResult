using GuessResult.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuessResult.DB.Models
{
    public class GREvent
    {
        public long Id { get; set; }

        public DateTime StartDate { get; set; }

        [StringLength(100)]
        [Required]
        public string HomeTeamName { get; set; }

        [StringLength(100)]
        [Required]
        public string AwayTeamName { get; set; }

        public byte? AwayTeamScore { get; set; }

        public byte? HomeTeamScore { get; set; }

        public long ExternalMatchId { get; set; }

        public bool IsDeleted { get; set; }

        public EventPredictionType PredictionType { get; set; }


        public virtual List<GRUserEvent> UserEvents { get; set; }
    }
}