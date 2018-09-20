using GuessResult.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessResult.DB.Models
{
    public class GRUserEvent
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long EventId { get; set; }

        public byte? HomeTeamScore { get; set; }

        public byte? AwayTeamScore { get; set; }

        public bool IsDeleted { get; set; }

        public GeneralScoreType? GeneralScoreType { get; set; }


        public virtual GRUser User { get; set; }

        public virtual GREvent Event { get; set; }

    }
}