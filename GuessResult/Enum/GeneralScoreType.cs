using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GuessResult.Enum
{
    public enum GeneralScoreType
    {
        [Description("Wygrana gospodarzy")]
        HomeTeamWin = 1,

        [Description("Wygrana gości")]
        AwayTeamWin = 2,

        [Description("Remmis")]
        Tie = 3
    }
}