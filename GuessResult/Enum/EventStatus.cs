using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GuessResult.Enum
{
    public enum EventStatus : byte
    {
        [Description("Zakończony")]
        Zakonczony = 1,

        [Description("Przyszły")]
        Przyszly = 2
    }
}