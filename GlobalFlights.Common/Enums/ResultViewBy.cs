using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalFlights.Common.Enums
{
    /*
     string
        view the flight destinations by COUNTRY, DATE, DESTINATION, DURATION, or WEEK. 
        View by COUNTRY to get the cheapest flight dates for every country in the given range.
        View by DATE to get the cheapest flight dates for every departure date in the given range.
        View by DURATION to get the cheapest flight dates for every departure date and for every duration in the given ranges.
        View by WEEK to get the cheapest flight destination for every week in the given range of departure dates
     */
    //DATE, DESTINATION, DURATION, WEEK, or COUNTRY
    public enum ResultViewBy
    {
        DATE,
        DURATION,
        DESTINATION,
        WEEK,
        COUNTRY
    }
}
