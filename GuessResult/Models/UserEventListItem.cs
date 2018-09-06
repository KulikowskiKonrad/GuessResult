using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessResult.Models
{
    public class UserEventListItem
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long EventId { get; set; }

        public byte HomeTeamScore { get; set; }

        public byte AwayTeamScore { get; set; }
    }
}