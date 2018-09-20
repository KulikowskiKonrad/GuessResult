using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GuessResult.Enum
{
    public enum EventPredictionType
    {
        [Description("Dokładny wynik")]
        ExactScore = 1,

        [Description("Ogólny wynik")]
        GeneralScore = 2
    }
}