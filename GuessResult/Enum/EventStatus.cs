using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GuessResult.Enum
{
    public enum EventStatus
    {
        [Description("Planowany")]
        Scheduled = 1,

        [Description("Skończony")]
        Finished = 2
    }
}