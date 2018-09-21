using GuessResult.Enum;
using GuessResult.Extensions;
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

        public byte? HomeTeamScore { get; set; }

        public byte? AwayTeamScore { get; set; }

        public string AwayTeamName { get; set; }

        public string HomeTeamName { get; set; }

        public DateTime StartDate { get; set; }

        public EventPredictionType? EventPredictionType { get; set; }

        public GeneralScoreType? GeneralScoreType { get; set; }
    }
}