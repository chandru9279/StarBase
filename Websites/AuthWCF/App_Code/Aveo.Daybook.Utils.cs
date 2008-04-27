using System;
using System.Diagnostics;


namespace Aveo.Daybook.Utils
{
    /// <summary>
    /// Types for handling Profile operations
    /// </summary>    
    public enum ProfileCalendarView
    {
        Invalid,
        Day,
        WorkWeek,
        Week,
        Custom,
    };

    public enum ProfileCalendarTimeFormat
    {
        Invalid,
        TwelveHour,
        TwentyFourHour,
    };

}